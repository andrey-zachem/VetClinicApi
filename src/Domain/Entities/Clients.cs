namespace Domain.Entities;

public class Clients
{
    public long ClientId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public long? UserId { get; set; }

    public User? User { get; set; }
    public ICollection<Pets> Pets { get; set; } = new List<Pets>();
    public ICollection<Invoices> Invoices { get; set; } = new List<Invoices>();
}
