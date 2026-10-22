namespace Domain.Entities;

public class PaymentMethods
{
    public short MethodId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Payments> Payments { get; set; } = new List<Payments>();
}
