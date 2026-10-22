namespace Application.DTOs.Prescriptions;

public class PrescriptionDto
{
    public long PrescriptionId { get; set; }
    public long VisitId { get; set; }
    public int MedicationId { get; set; }
    public string Dosage { get; set; } = string.Empty;
    public string? Frequency { get; set; }
    public short? DurationDays { get; set; }
    public string? Instructions { get; set; }
}
