namespace Application.DTOs.Invoices;

public class InvoiceCreateDto
{
    public long VisitId { get; set; }
    public long ClientId { get; set; }
    public long? CreatedBy { get; set; }
    public IReadOnlyList<InvoiceItemCreateDto> Items { get; set; } = [];
}
