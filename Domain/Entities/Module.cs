using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("Telemetries")]
public abstract class Module
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}
