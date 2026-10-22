namespace Application.DTOs.Invoices;

public class PaymentCreateDto
{
    public long InvoiceId { get; set; }
    public short MethodId { get; set; }
    public decimal Amount { get; set; }
}
