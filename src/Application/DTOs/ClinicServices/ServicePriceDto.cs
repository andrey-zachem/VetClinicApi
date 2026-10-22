namespace Application.DTOs.ClinicServices;

public class ServicePriceDto
{
    public long PriceId { get; set; }
    public int ServiceId { get; set; }
    public decimal Price { get; set; }
    public DateOnly ValidFrom { get; set; }
}
