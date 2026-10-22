namespace Domain.Entities;

public class AuditLog
{
    public long AuditId { get; set; }
    public long? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public string RecordId { get; set; } = string.Empty;
    public string? Changes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public User? User { get; set; }
}
