namespace Domain.Entities;
public class Telemetry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public int DeviceId { get; set; }
    public Device Device { get; set; }
}
