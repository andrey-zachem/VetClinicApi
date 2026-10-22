namespace Domain.Entities;

public class Medications
{
    public int MedicationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Form { get; set; }
    public string? ActiveSubstance { get; set; }

    public ICollection<Prescriptions> Prescriptions { get; set; } = new List<Prescriptions>();
}
