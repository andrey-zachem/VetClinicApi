namespace Domain.Entities;

public class WorkSchedule
{
    public long ScheduleId { get; set; }
    public long EmployeeId { get; set; }
    public short WeekDay { get; set; }
    public TimeOnly StartsAt { get; set; }
    public TimeOnly EndsAt { get; set; }

    public Employees Employee { get; set; } = null!;
}
