using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Infrastructure.Data;
public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    internal async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}\nAn error occurred while initialising the database.");
            throw;
        }
    }

    internal async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex} --- An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (!_context.Units.Any())
        {
            _context.Units.Add(new Unit
            {
                Modules = new List<Module>
                {
                    new ModuleA
                    {
                        Date = DateTime.Now,
                        Prop1 = "value1",
                        Prop2 = "value2",
                        Prop3 = "value3",
                    },
                    new ModuleB
                    {
                        Date = DateTime.Now,
                        Prop4 = "value4",
                        Prop5 = "value5",
                        Prop6 = "value6",
                    },
                    new ModuleC
                    {
                        Date = DateTime.Now,
                        Prop7 = "value7",
                        Prop8 = "value8",
                        Prop9 = "value9",
                    },
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
