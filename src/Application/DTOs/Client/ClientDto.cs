namespace Application.DTOs.Client;

public class ClientDto
{
    public long ClientId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; }
    public long? UserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
