using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Unit> Units { get; set; }
    public DbSet<Domain.Entities.Device> Devices { get; set; } // Device
    public DbSet<Telemetry> Telemetries { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
