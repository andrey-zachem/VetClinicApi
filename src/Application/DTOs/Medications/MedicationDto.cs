namespace Application.DTOs.Medications;

public class MedicationDto
{
    public int MedicationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Form { get; set; }
    public string? ActiveSubstance { get; set; }
}
