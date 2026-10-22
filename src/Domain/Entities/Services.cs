namespace Domain.Entities;

public class Services
{
    public int ServiceId { get; set; }
    public short CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public short? DurationMin { get; set; }
    public bool IsActive { get; set; } = true;

    public ServiceCategories Category { get; set; } = null!;
    public ICollection<ServicePrices> Prices { get; set; } = new List<ServicePrices>();
    public ICollection<InvoiceItems> InvoiceItems { get; set; } = new List<InvoiceItems>();
}
