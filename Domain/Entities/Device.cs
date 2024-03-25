namespace Domain.Entities;
public class Device
{
    public int Id { get; set; }
    public string Type { get; set; }
    public List<Telemetry> Telemetries { get; set; } = new List<Telemetry>();

    public int UnitId { get; set; }
    public Unit Unit { get; set; }
}
