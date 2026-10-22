namespace Application.DTOs.Invoices;

public class PaymentMethodDto
{
    public short MethodId { get; set; }
    public string Name { get; set; } = string.Empty;
}
