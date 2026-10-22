namespace Application.DTOs.Visits;

public class VisitDiagnosisDto
{
    public int DiagnosisId { get; set; }
    public string? DiagnosisName { get; set; }
    public bool IsPrimary { get; set; }
}
