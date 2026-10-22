namespace Domain.Entities;

public class Rooms
{
    public short RoomId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
}
