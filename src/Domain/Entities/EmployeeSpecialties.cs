namespace Domain.Entities;

public class EmployeeSpecialties
{
    public long EmployeeId { get; set; }
    public Employees Employee { get; set; } = null!;

    public short SpecialtyId { get; set; }
    public Specialties Specialty { get; set; } = null!;
}
