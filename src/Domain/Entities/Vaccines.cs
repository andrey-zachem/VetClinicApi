namespace Domain.Entities;

public class Vaccines
{
    public int VaccineId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Manufacturer { get; set; }
    public string ProtectsAgainst { get; set; } = string.Empty;
    public int ValidityDays { get; set; }

    public ICollection<VaccineBatches> Batches { get; set; } = new List<VaccineBatches>();
}
