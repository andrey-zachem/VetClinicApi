namespace Application.DTOs.Appointments;

public class AppointmentDto
{
    public long AppointmentId { get; set; }
    public long PetId { get; set; }
    public long EmployeeId { get; set; }
    public short? RoomId { get; set; }
    public string? RoomName { get; set; }
    public short StatusId { get; set; }
    public string? StatusName { get; set; }
    public DateTimeOffset StartsAt { get; set; }
    public DateTimeOffset EndsAt { get; set; }
    public string? Reason { get; set; }
    public long? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
