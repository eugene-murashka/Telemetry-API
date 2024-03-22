using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Domain.Entities.Module> Modules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Domain.Entities.Module>()
            .HasDiscriminator<string>("Type")
            .HasValue<ModuleA>("ModuleA")
            .HasValue<ModuleB>("ModuleB")
            .HasValue<ModuleC>("ModuleC");

        base.OnModelCreating(builder);
    }
}
