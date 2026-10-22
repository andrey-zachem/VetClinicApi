namespace Domain.Entities;

public class Payments
{
    public long PaymentId { get; set; }
    public long InvoiceId { get; set; }
    public short MethodId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset PaidAt { get; set; }

    public Invoices Invoice { get; set; } = null!;
    public PaymentMethods Method { get; set; } = null!;
}
