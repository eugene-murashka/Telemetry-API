using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Unit> Units { get; set; }
    DbSet<Domain.Entities.Device> Devices { get; set; } // Device
    DbSet<Telemetry> Telemetries { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
