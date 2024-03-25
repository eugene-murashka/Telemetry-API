using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
app.MapPost("/api/unit", (Unit newUnit) =>
{
    return "createUnit";
})
.WithName("CreateUnit")
.WithOpenApi();

// Редактировать таргетную сущность или включая все вложенные?
app.MapPut("/api/unit", (Unit newUnit) =>
{
    return "updateUnit";
})
.WithName("UpdateUnit")
.WithOpenApi();

app.MapDelete("/api/unit", (int id) =>
{
    return "deleteUnit";
})
.WithName("DeleteUnit")
.WithOpenApi();

app.MapGet("/api/telemetry", (int id) =>
{
    return "getTelemetry";
})
.WithName("GetTelemetry")
.WithOpenApi();

app.MapPost("/api/telemetry", (Telemetry newTelemetry) =>
{
    return "CreateTelemetry";
})
.WithName("CreateTelemetry")
.WithOpenApi();

app.MapPut("/api/telemetry", (Telemetry newTelemetry) =>
{
    return "UpdateTelemetry";
})
.WithName("UpdateTelemetry")
.WithOpenApi();

app.MapDelete("/api/telemetry", (int id) =>
{
    return "DeleteTelemetry";
})
.WithName("DeleteTelemetry")
.WithOpenApi();

app.MapGet("/api/telemetries", ([AsParameters] GetTelemetriesQuery query) =>
{
    return "GetTelemetries";
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
