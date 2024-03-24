using Domain.Entities;
using Infrastructure.Data;

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

#region Data for testing

var Units = new List<Unit>();
var UnitsItems = new List<Unit>
{
    new Unit
    {
        Id = 1,
        Modules = new List<Module>(),
    },
    new Unit
    {
        Id = 2,
        Modules = new List<Module>(),
    }
};
Units.AddRange(UnitsItems);

var ModuleItems1 = new List<Module>
{
    new Module
    {
        Id = 1,
        Type = "0",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[0],
        UnitId = 1,
    },
    new Module
    {
        Id = 2,
        Type = "1",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[0],
        UnitId = 1,
    },
    new Module
    {
        Id = 3,
        Type = "2",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[0],
        UnitId = 1,
    },
};
Units[0].Modules.AddRange(ModuleItems1);
var ModuleItems2 = new List<Module>
{
    new Module
    {
        Id = 4,
        Type = "0",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[1],
        UnitId = 2,
    },
    new Module
    {
        Id = 5,
        Type = "1",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[1],
        UnitId = 2,
    },
    new Module
    {
        Id = 6,
        Type = "2",
        Telemetries = new List<Telemetry>(),
        //Unit = Units[1],
        UnitId = 2,
    },
};
Units[1].Modules.AddRange(ModuleItems2);

var TelemetryItems1 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 1,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value1",
        //Module = Units[0].Modules[0],
        ModuleId = 1,
    },
    new Telemetry
    {
        Id = 2,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value2",
        //Module = Units[0].Modules[0],
        ModuleId = 1,
    },
    new Telemetry
    {
        Id = 3,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value3",
        //Module = Units[0].Modules[0],
        ModuleId = 1,
    },
};
Units[0].Modules[0].Telemetries.AddRange(TelemetryItems1);

var TelemetryItems2 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 4,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value4",
        //Module = Units[0].Modules[1],
        ModuleId = 2,
    },
    new Telemetry
    {
        Id = 5,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value5",
        //Module = Units[0].Modules[1],
        ModuleId = 2,
    },
    new Telemetry
    {
        Id = 6,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value6",
        //Module = Units[0].Modules[1],
        ModuleId = 2,
    },
};
Units[0].Modules[1].Telemetries.AddRange(TelemetryItems2);

var TelemetryItems3 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 7,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value7",
        //Module = Units[0].Modules[2],
        ModuleId = 3,
    },
    new Telemetry
    {
        Id = 8,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value8",
        //Module = Units[0].Modules[2],
        ModuleId = 3,
    },
    new Telemetry
    {
        Id = 9,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value9",
        //Module = Units[0].Modules[2],
        ModuleId = 3,
    },
};
Units[0].Modules[2].Telemetries.AddRange(TelemetryItems3);

var TelemetryItems4 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 1,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value10",
        //Module = Units[1].Modules[0],
        ModuleId = 4,
    },
    new Telemetry
    {
        Id = 2,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value20",
        //Module = Units[1].Modules[0],
        ModuleId = 4,
    },
    new Telemetry
    {
        Id = 3,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value30",
        //Module = Units[1].Modules[0],
        ModuleId = 4,
    },
};
Units[1].Modules[0].Telemetries.AddRange(TelemetryItems4);

var TelemetryItems5 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 4,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value40",
        //Module = Units[1].Modules[1],
        ModuleId = 5,
    },
    new Telemetry
    {
        Id = 5,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value50",
        //Module = Units[1].Modules[1],
        ModuleId = 5,
    },
    new Telemetry
    {
        Id = 6,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value60",
        //Module = Units[1].Modules[1],
        ModuleId = 5,
    },
};
Units[1].Modules[1].Telemetries.AddRange(TelemetryItems5);

var TelemetryItems6 = new List<Telemetry>
{
    new Telemetry
    {
        Id = 7,
        Date = DateTime.Now,
        Key = "prop1",
        Value = "value70",
        //Module = Units[1].Modules[2],
        ModuleId = 6,
    },
    new Telemetry
    {
        Id = 8,
        Date = DateTime.Now,
        Key = "prop2",
        Value = "value80",
        //Module = Units[1].Modules[2],
        ModuleId = 6,
    },
    new Telemetry
    {
        Id = 9,
        Date = DateTime.Now,
        Key = "prop3",
        Value = "value90",
        //Module = Units[1].Modules[2],
        ModuleId = 6,
    },
};
Units[1].Modules[2].Telemetries.AddRange(TelemetryItems6);

#endregion

app.MapGet("/api/unit", (int id) =>
{
    return Units
        .Where(unit => unit.Id == id)
        .Select(unit => new UnitDto
        {
            Id = unit.Id,
            Telemetries = unit.Modules.SelectMany(module => module.Telemetries, (module, telemetry) => new TelemetryDto
            {
                ModuleType = module.Type,
                Date = telemetry.Date,
                Key = telemetry.Key,
                Value = telemetry.Value,
            })
            .ToList(),
        })
        .ToArray();
})
.WithName("GetUnit")
.WithOpenApi();

// Добавляется только сущность или можно и все вложенные сущности?
app.MapPost("/api/unit", (Unit newUnit) =>
{
    newUnit.Id = Units.Max(unit => unit.Id) + 1;
    Units.Add(newUnit);

    var results = new UnitDto
    {
        Id = newUnit.Id,
        Telemetries = newUnit.Modules.SelectMany(module => module.Telemetries, (module, telemetry) => new TelemetryDto
        {
            ModuleType = module.Type,
            Date = telemetry.Date,
            Key = telemetry.Key,
            Value = telemetry.Value,
        })
    .ToList(),
    };

    return results;
})
.WithName("CreateUnit")
.WithOpenApi();

// Редактировать таргетную сущность или включая все вложенные?
app.MapPut("/api/unit", (Unit newUnit) =>
{
    var oldUnit = Units.FirstOrDefault(unit => unit.Id == newUnit.Id);
    oldUnit = newUnit;

    var results = new UnitDto
    {
        Id = newUnit.Id,
        Telemetries = newUnit.Modules.SelectMany(module => module.Telemetries, (module, telemetry) => new TelemetryDto
        {
            ModuleType = module.Type,
            Date = telemetry.Date,
            Key = telemetry.Key,
            Value = telemetry.Value,
        })
        .ToList(),
    };

    return results;
})
.WithName("UpdateUnit")
.WithOpenApi();

app.MapDelete("/api/unit", (int id) =>
{
    var unitForRemoving = Units.FirstOrDefault(unit => unit.Id == id);

    Units.Remove(unitForRemoving);

    var results = new UnitDto
    {
        Id = unitForRemoving.Id,
        Telemetries = unitForRemoving.Modules.SelectMany(module => module.Telemetries, (module, telemetry) => new TelemetryDto
        {
            ModuleType = module.Type,
            Date = telemetry.Date,
            Key = telemetry.Key,
            Value = telemetry.Value,
        })
    .ToList(),
    };

    return results;
})
.WithName("DeleteUnit")
.WithOpenApi();

app.MapGet("/api/telemetry", (int id) =>
{
    var telemetry = Units
        .SelectMany(unit => unit.Modules)
        .SelectMany(module => module.Telemetries)
        .FirstOrDefault(telemetry => telemetry.Id == id);

    var results = new TelemetryDto
    {
        ModuleType = Units.SelectMany(unit => unit.Modules).FirstOrDefault(module => module.Id == telemetry.ModuleId).Type,
        Date = telemetry.Date,
        Key = telemetry.Key,
        Value = telemetry.Value,
    };

    return results;
})
.WithName("GetTelemetry")
.WithOpenApi();

app.MapPost("/api/telemetry", (Telemetry newTelemetry) =>
{
    var module = Units.SelectMany(unit => unit.Modules).FirstOrDefault(module => module.Id == newTelemetry.ModuleId);
    newTelemetry.Id = Units.SelectMany(unit => unit.Modules).SelectMany(module => module.Telemetries).Max(telemetry => telemetry.Id) + 1;
    module.Telemetries.Add(newTelemetry);

    return newTelemetry;
})
.WithName("CreateTelemetry")
.WithOpenApi();

app.MapPut("/api/telemetry", (Telemetry newTelemetry) =>
{
    var module = Units.SelectMany(unit => unit.Modules).FirstOrDefault(module => module.Id == newTelemetry.ModuleId);
    var telemetry = Units.SelectMany(unit => unit.Modules).SelectMany(module => module.Telemetries).FirstOrDefault(telemetry => telemetry.Id == newTelemetry.Id);

    telemetry.Date = newTelemetry.Date;
    telemetry.Key = newTelemetry.Key;
    telemetry.Value = newTelemetry.Value;
    telemetry.ModuleId = newTelemetry.ModuleId;
    telemetry.Module = newTelemetry.Module;

    return new TelemetryDto
    {
        ModuleType = module.Type,
        Date = telemetry.Date,
        Key = telemetry.Key,
        Value = telemetry.Value,
    };
})
.WithName("UpdateTelemetry")
.WithOpenApi();

app.MapDelete("/api/telemetry", (int id) =>
{
    var telemetryForDelete = Units.SelectMany(unit => unit.Modules).SelectMany(module => module.Telemetries).FirstOrDefault(telemetry => telemetry.Id == id);
    var module = Units.SelectMany(unit => unit.Modules).FirstOrDefault(module => module.Id == telemetryForDelete.ModuleId);
    module.Telemetries.Remove(telemetryForDelete);

    return new TelemetryDto
    {
        ModuleType = module.Type,
        Date = telemetryForDelete.Date,
        Key = telemetryForDelete.Key,
        Value = telemetryForDelete.Value,
    };
})
.WithName("DeleteTelemetry")
.WithOpenApi();

app.MapGet("/api/telemetries", ([AsParameters] GetTelemetriesQuery query) =>
{
    var result = Units
        .SelectMany(u => u.Modules)
        .Where(m => m.Type == query.ModuleType)
        .SelectMany(m => m.Telemetries, (m, t) => new TelemetryDto
        {
            ModuleType = m.Type,
            Date = t.Date,
            Key = t.Key,
            Value = t.Value,
        })
        .Where(tdto => tdto.Date >= query.DateFrom && tdto.Date <= query.DateTo)
        .Where(tdto => tdto.Key == query.Status)
        .ToArray();

    return result;
})
.WithName("GetTelemetries")
.WithOpenApi();


app.Run();

#region Queries structures
public record GetTelemetriesQuery
{
    public string ModuleType { get; init; }
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }
    public string Status { get; init; }
}
#endregion

#region Dtos
public class TelemetryDto
{
    public string ModuleType { get; init; }
    public DateTime Date { get; init; }
    public string Key { get; init; }
    public string Value { get; init; }
}

public class UnitDto
{
    public int Id { get; init; }
    public List<TelemetryDto> Telemetries { get; init; }
}
#endregion

#region Entities
public class Telemetry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public int ModuleId { get; set; }
    public Module Module { get; set; }
}

public class Module
{
    public int Id { get; set; }
    public string Type { get; set; }
    public List<Telemetry> Telemetries { get; set; } = new List<Telemetry>();

    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}

public class Unit
{
    public int Id { get; set; }
    public List<Module> Modules { get; set; } = new List<Module>();
}
#endregion
