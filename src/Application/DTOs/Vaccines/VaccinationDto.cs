namespace Application.DTOs.Vaccines;

public class VaccinationDto
{
    public long VaccinationId { get; set; }
    public long PetId { get; set; }
    public long BatchId { get; set; }
    public long EmployeeId { get; set; }
    public long? VisitId { get; set; }
    public DateOnly VaccinatedOn { get; set; }
    public string? Note { get; set; }
}
