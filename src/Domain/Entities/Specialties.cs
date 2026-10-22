namespace Domain.Entities;

public class Specialties
{
    public short SpecialtyId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<EmployeeSpecialties> EmployeeSpecialties { get; set; } = new List<EmployeeSpecialties>();
}
