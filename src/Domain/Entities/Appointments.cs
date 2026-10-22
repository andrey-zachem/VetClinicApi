namespace Domain.Entities;

public class Appointments
{
    public long AppointmentId { get; set; }
    public long PetId { get; set; }
    public long EmployeeId { get; set; }
    public short? RoomId { get; set; }
    public short StatusId { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset EndsAt { get; set; }
    public string? Reason { get; set; }
    public long? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Pets Pet { get; set; } = null!;
    public Employees Employee { get; set; } = null!;
    public Rooms? Room { get; set; }
    public AppointmentStatuses Status { get; set; } = null!;
    public User? CreatedByUser { get; set; }
    public Visits? Visit { get; set; }
}
