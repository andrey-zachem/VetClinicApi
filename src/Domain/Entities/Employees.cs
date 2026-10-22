namespace Domain.Entities;

public class Employees
{
    public long EmployeeId { get; set; }
    public long? UserId { get; set; }
    public short PositionId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateOnly HiredOn { get; set; }
    public DateOnly? FiredOn { get; set; }

    public User? User { get; set; }
    public Positions Position { get; set; } = null!;
    public ICollection<EmployeeSpecialties> EmployeeSpecialties { get; set; } = new List<EmployeeSpecialties>();
    public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
    public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
    public ICollection<Visits> Visits { get; set; } = new List<Visits>();
    public ICollection<Vaccinations> Vaccinations { get; set; } = new List<Vaccinations>();
    public ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();
}
