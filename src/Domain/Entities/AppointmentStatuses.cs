namespace Domain.Entities;

public class AppointmentStatuses
{
    public short StatusId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
}
