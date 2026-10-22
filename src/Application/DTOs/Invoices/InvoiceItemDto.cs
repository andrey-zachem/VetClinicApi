namespace Application.DTOs.Invoices;

public class InvoiceItemDto
{
    public long ItemId { get; set; }
    public int ServiceId { get; set; }
    public long? EmployeeId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal LineTotal { get; set; }
}
