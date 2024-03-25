using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.InitialiseDatabaseAsync();
}

app.MapGet("/api/unit", async (IApplicationDbContext context, int id) =>
{
    return await context.Units
        .Where(unit => unit.Id == id)
        .Select(unit => new UnitDto
        {
            Id = unit.Id,
            Telemetries = unit.Devices
                .SelectMany(device => device.Telemetries, (device, telemetry) => new TelemetryDto
                {
                    Id = telemetry.Id,
                    DeviceType = device.Type,
                    Date = telemetry.Date,
                    Key = telemetry.Key,
                    Value = telemetry.Value,
                }),
        })
        .AsNoTracking()
        .FirstOrDefaultAsync();
})
.WithName("GetUnit")
.WithOpenApi();

// Добавляется только сущность или можно и все вложенные сущности?
app.MapPost("/api/unit", async (IApplicationDbContext context, CancellationToken cancellationToken, Unit newUnit) =>
{
    context.Units.Add(newUnit);
    await context.SaveChangesAsync(cancellationToken);

    return new UnitDto
    {
        Id = newUnit.Id,
        Telemetries = newUnit.Devices
            .SelectMany(device => device.Telemetries, (device, telemetry) => new TelemetryDto
            {
                Id = telemetry.Id,
                DeviceType = device.Type,
                Date = telemetry.Date,
                Key = telemetry.Key,
                Value = telemetry.Value,
            }),
    };
})
.WithName("CreateUnit")
.WithOpenApi();

app.MapDelete("/api/unit", async (IApplicationDbContext context, CancellationToken cancellationToken, int id) =>
{
    var unitToDelete = await context.Units
        .FindAsync(new object[] { id }, cancellationToken);

    context.Units.Remove(unitToDelete);

    await context.SaveChangesAsync(cancellationToken);

    return new UnitDto
    {
        Id = unitToDelete.Id,
        Telemetries = unitToDelete.Devices
            .SelectMany(device => device.Telemetries, (device, telemetry) => new TelemetryDto
            {
                Id = telemetry.Id,
                DeviceType = device.Type,
                Date = telemetry.Date,
                Key = telemetry.Key,
                Value = telemetry.Value,
            }),
    };
})
.WithName("DeleteUnit")
.WithOpenApi();

app.MapGet("/api/telemetry", async (IApplicationDbContext context, int id) =>
{
    return await context.Telemetries
        .Where(telemetry => telemetry.Id == id)
        .Select(telemetry => new TelemetryDto
        {
            Id = telemetry.Id,
            DeviceType = telemetry.Device.Type,
            Date = telemetry.Date,
            Key = telemetry.Key,
            Value = telemetry.Value,
        })
        .AsNoTracking()
        .FirstOrDefaultAsync();
})
.WithName("GetTelemetry")
.WithOpenApi();

app.MapPost("/api/telemetry", async (IApplicationDbContext context, CancellationToken cancellationToken, Telemetry newTelemetry) =>
{
    context.Telemetries.Add(newTelemetry);
    await context.SaveChangesAsync(cancellationToken);

    var device = await context.Devices.FindAsync(new object[] { newTelemetry.DeviceId });

    return new TelemetryDto
    {
        Id = newTelemetry.Id,
        DeviceType = device.Type,
        Date = newTelemetry.Date,
        Key = newTelemetry.Key,
        Value = newTelemetry.Value,
    };
})
.WithName("CreateTelemetry")
.WithOpenApi();

app.MapPut("/api/telemetry", async (IApplicationDbContext context, CancellationToken cancellationToken, Telemetry newTelemetry) =>
{
    var oldTelemetry = await context.Telemetries
        .FindAsync(new object[] { newTelemetry.Id }, cancellationToken);

    var device = await context.Devices
        .FindAsync(new object[] { oldTelemetry.DeviceId }, cancellationToken);
    
    oldTelemetry.Date = newTelemetry.Date;
    oldTelemetry.Value = newTelemetry.Value;

    context.SaveChangesAsync(cancellationToken);

    return new TelemetryDto
    {
        Id = oldTelemetry.Id,
        DeviceType = device.Type,
        Date = oldTelemetry.Date,
        Key = oldTelemetry.Key,
        Value = oldTelemetry.Value,
    };
})
.WithName("UpdateTelemetry")
.WithOpenApi();

app.MapDelete("/api/telemetry", async (IApplicationDbContext context, CancellationToken cancellationToken, int id) =>
{
    var telemetryToDelete = await context.Telemetries
        .FindAsync(new object[] { id }, cancellationToken);

    var device = await context.Devices
        .FindAsync(new object[] { telemetryToDelete.DeviceId }, cancellationToken);

    context.Telemetries.Remove(telemetryToDelete);

    await context.SaveChangesAsync(cancellationToken);

    return new TelemetryDto
    {
        Id = telemetryToDelete.Id,
        DeviceType = device.Type,
        Date = telemetryToDelete.Date,
        Key = telemetryToDelete.Key,
        Value = telemetryToDelete.Value,
    };
})
.WithName("DeleteTelemetry")
.WithOpenApi();

app.MapGet("/api/telemetries", async (IApplicationDbContext context, CancellationToken cancellationToken, [AsParameters] GetTelemetriesQuery query) =>
{
    return await context.Telemetries
        .Where(telemetry => telemetry.Device.Type == query.DeviceType)
        .Where(telemetry => telemetry.Date >= query.DateFrom && telemetry.Date <= query.DateTo)
        .Where(telemetry => telemetry.Key == query.Status)
        .Select(telemetry => new TelemetryDto
        {
            Id = telemetry.Id,
            DeviceType = telemetry.Device.Type,
            Date = telemetry.Date,
            Key = telemetry.Key,
            Value = telemetry.Value,
        })
        .AsNoTracking()
        .ToListAsync();
})
.WithName("GetTelemetries")
.WithOpenApi();

app.Run();

#region Queries structures
public record GetTelemetriesQuery
{
    public string DeviceType { get; init; }
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }
    public string Status { get; init; }
}
#endregion

#region Dtos
public class TelemetryDto
{
    public int Id { get; set; }
    public string DeviceType { get; init; }
    public DateTime Date { get; init; }
    public string Key { get; init; }
    public string Value { get; init; }
}

public class UnitDto
{
    public int Id { get; init; }
    public IEnumerable<TelemetryDto> Telemetries { get; init; }
}
#endregion
