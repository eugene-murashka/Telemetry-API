namespace Domain.Entities;
public class Unit
{
    public int Id { get; set; }
    public List<Device> Devices { get; set; } = new List<Device>();
}
