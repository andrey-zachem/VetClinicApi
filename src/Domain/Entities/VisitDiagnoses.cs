namespace Domain.Entities;

public class VisitDiagnoses
{
    public long VisitId { get; set; }
    public int DiagnosisId { get; set; }
    public bool IsPrimary { get; set; }

    public Visits Visit { get; set; } = null!;
    public Diagnoses Diagnosis { get; set; } = null!;
}
