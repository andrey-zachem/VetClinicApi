namespace Domain.Entities;

public class Visits
{
    public long VisitId { get; set; }
    public long? AppointmentId { get; set; }
    public long PetId { get; set; }
    public long EmployeeId { get; set; }
    public DateTimeOffset VisitedAt { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? TemperatureC { get; set; }
    public string? Complaints { get; set; }
    public string? Anamnesis { get; set; }
    public string? Examination { get; set; }
    public string? TreatmentPlan { get; set; }
    public string? Recommendations { get; set; }

    public Appointments? Appointment { get; set; }
    public Pets Pet { get; set; } = null!;
    public Employees Employee { get; set; } = null!;
    public ICollection<VisitDiagnoses> Diagnoses { get; set; } = new List<VisitDiagnoses>();
    public ICollection<VisitAttachments> Attachments { get; set; } = new List<VisitAttachments>();
    public ICollection<Prescriptions> Prescriptions { get; set; } = new List<Prescriptions>();
    public ICollection<Vaccinations> Vaccinations { get; set; } = new List<Vaccinations>();
    public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
}
