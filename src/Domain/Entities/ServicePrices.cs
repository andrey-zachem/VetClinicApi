namespace Domain.Entities;

public class ServicePrices
{
    public long PriceId { get; set; }
    public int ServiceId { get; set; }
    public decimal Price { get; set; }
    public DateOnly ValidFrom { get; set; }

    public Services Service { get; set; } = null!;
}
