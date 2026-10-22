namespace Domain.Entities;

public class Invoices
{
    public long InvoiceId { get; set; }
    public long VisitId { get; set; }
    public long ClientId { get; set; }
    public DateTimeOffset IssuedAt { get; set; }
    public long? CreatedBy { get; set; }

    public Visits Visit { get; set; } = null!;
    public Clients Client { get; set; } = null!;
    public User? CreatedByUser { get; set; }
    public ICollection<InvoiceItems> Items { get; set; } = new List<InvoiceItems>();
    public ICollection<Payments> Payments { get; set; } = new List<Payments>();
}
