namespace Application.DTOs.Visits;

public class VisitCreateDto
{
    public long? AppointmentId { get; set; }
    public long PetId { get; set; }
    public long EmployeeId { get; set; }
    public DateTimeOffset? VisitedAt { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? TemperatureC { get; set; }
    public string? Complaints { get; set; }
    public string? Anamnesis { get; set; }
    public string? Examination { get; set; }
    public string? TreatmentPlan { get; set; }
    public string? Recommendations { get; set; }
    public IReadOnlyList<VisitDiagnosisDto> Diagnoses { get; set; } = [];
}
