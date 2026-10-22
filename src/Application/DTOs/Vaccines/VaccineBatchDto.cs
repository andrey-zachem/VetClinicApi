namespace Application.DTOs.Vaccines;

public class VaccineBatchDto
{
    public long BatchId { get; set; }
    public int VaccineId { get; set; }
    public string BatchNo { get; set; } = string.Empty;
    public DateOnly ExpiresOn { get; set; }
}
