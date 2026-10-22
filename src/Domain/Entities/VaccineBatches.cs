namespace Domain.Entities;

public class VaccineBatches
{
    public long BatchId { get; set; }
    public int VaccineId { get; set; }
    public string BatchNo { get; set; } = string.Empty;
    public DateOnly ExpiresOn { get; set; }

    public Vaccines Vaccine { get; set; } = null!;
    public ICollection<Vaccinations> Vaccinations { get; set; } = new List<Vaccinations>();
}
