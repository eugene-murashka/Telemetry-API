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
            var Units = new List<Unit>();

            #region creating testing data
            var UnitsItems = new List<Unit>
            {
                new Unit
                {
                    Devices = new List<Device>(),
                },
                new Unit
                {
                    Devices = new List<Device>(),
                }
            };
            Units.AddRange(UnitsItems);

            var DeviceItems1 = new List<Device>
            {
                new Device
                {
                    Type = "0",
                    Telemetries = new List<Telemetry>(),
                },
                new Device
                {
                    Type = "1",
                    Telemetries = new List<Telemetry>(),
                },
                new Device
                {
                    Type = "2",
                    Telemetries = new List<Telemetry>(),
                },
            };
            Units[0].Devices.AddRange(DeviceItems1);
            var DeviceItem2 = new List<Device>
            {
                new Device
                {
                    Type = "0",
                    Telemetries = new List<Telemetry>(),
                },
                new Device
                {
                    Type = "1",
                    Telemetries = new List<Telemetry>(),
                },
                new Device
                {
                    Type = "2",
                    Telemetries = new List<Telemetry>(),
                },
            };
            Units[1].Devices.AddRange(DeviceItem2);

            var TelemetryItems1 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value1",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value2",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value3",
                },
            };
            Units[0].Devices[0].Telemetries.AddRange(TelemetryItems1);

            var TelemetryItems2 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value4",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value5",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value6",
                },
            };
            Units[0].Devices[1].Telemetries.AddRange(TelemetryItems2);

            var TelemetryItems3 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value7",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value8",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value9",
                },
            };
            Units[0].Devices[2].Telemetries.AddRange(TelemetryItems3);

            var TelemetryItems4 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value10",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value20",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value30",
                },
            };
            Units[1].Devices[0].Telemetries.AddRange(TelemetryItems4);

            var TelemetryItems5 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value40",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value50",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value60",
                },
            };
            Units[1].Devices[1].Telemetries.AddRange(TelemetryItems5);

            var TelemetryItems6 = new List<Telemetry>
            {
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop1",
                    Value = "value70",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop2",
                    Value = "value80",
                },
                new Telemetry
                {
                    Date = DateTime.Now,
                    Key = "prop3",
                    Value = "value90",
                },
            };
            Units[1].Devices[2].Telemetries.AddRange(TelemetryItems6);
            #endregion

            _context.Units.AddRange(Units);

            await _context.SaveChangesAsync();
        }
    }
}
