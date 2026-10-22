namespace Application.DTOs.Employees.WorkSchedules;

public class WorkScheduleDto
{
    public long ScheduleId { get; set; }
    public long EmployeeId { get; set; }
    public short WeekDay { get; set; }
    public TimeOnly StartsAt { get; set; }
    public TimeOnly EndsAt { get; set; }
}
