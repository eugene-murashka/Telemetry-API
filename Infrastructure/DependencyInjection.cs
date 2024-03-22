using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "DataSource=app.db;Cache=Shared";

        services.AddSqlite<ApplicationDbContext>(connectionString);
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
