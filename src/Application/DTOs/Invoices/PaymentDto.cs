namespace Application.DTOs.Invoices;

public class PaymentDto
{
    public long PaymentId { get; set; }
    public long InvoiceId { get; set; }
    public short MethodId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset PaidAt { get; set; }
}
