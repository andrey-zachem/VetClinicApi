namespace Domain.Entities;

public class Diagnoses
{
    public int DiagnosisId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<VisitDiagnoses> VisitDiagnoses { get; set; } = new List<VisitDiagnoses>();
}
