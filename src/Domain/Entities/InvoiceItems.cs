namespace Domain.Entities;

public class InvoiceItems
{
    public long ItemId { get; set; }
    public long InvoiceId { get; set; }
    public int ServiceId { get; set; }
    public long? EmployeeId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }

    public Invoices Invoice { get; set; } = null!;
    public Services Service { get; set; } = null!;
    public Employees? Employee { get; set; }
}
