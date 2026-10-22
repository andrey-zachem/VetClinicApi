namespace Application.DTOs.Employees.WorkSchedules;

public class WorkScheduleCreate
{
    public long EmployeeId { get; set; }
    public short WeekDay { get; set; }
    public TimeOnly StartsAt { get; set; }
    public TimeOnly EndsAt { get; set; }
}
