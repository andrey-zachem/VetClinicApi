namespace Domain.Entities;

public class Vaccinations
{
    public long VaccinationId { get; set; }
    public long PetId { get; set; }
    public long BatchId { get; set; }
    public long EmployeeId { get; set; }
    public long? VisitId { get; set; }
    public DateOnly VaccinatedOn { get; set; }
    public string? Note { get; set; }

    public Pets Pet { get; set; } = null!;
    public VaccineBatches Batch { get; set; } = null!;
    public Employees Employee { get; set; } = null!;
    public Visits? Visit { get; set; }
}
