namespace Application.DTOs.Employees;

public class EmployeesDto
{
    public long EmployeeId { get; set; }
    public long? UserId { get; set; }
    public short PositionId { get; set; }
    public string? PositionName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateOnly HiredOn { get; set; }
    public DateOnly? FiredOn { get; set; }
    public IReadOnlyList<short> SpecialtyIds { get; set; } = [];
}
