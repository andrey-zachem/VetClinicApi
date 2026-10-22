namespace Application.DTOs.Invoices;

public class InvoiceDto
{
    public long InvoiceId { get; set; }
    public long VisitId { get; set; }
    public long ClientId { get; set; }
    public DateTimeOffset IssuedAt { get; set; }
    public long? CreatedBy { get; set; }
    public decimal Total { get; set; }
    public decimal Paid { get; set; }
    public IReadOnlyList<InvoiceItemDto> Items { get; set; } = [];
    public IReadOnlyList<PaymentDto> Payments { get; set; } = [];
}
