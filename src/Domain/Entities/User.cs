namespace Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset? LastLoginAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Clients? Client { get; set; }
    public ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
    public ICollection<Employees> Employees { get; set; } = new List<Employees>();
    public ICollection<Appointments> CreatedAppointments { get; set; } = new List<Appointments>();
    public ICollection<Invoices> CreatedInvoices { get; set; } = new List<Invoices>();
    public ICollection<FileObjects> UploadedFiles { get; set; } = new List<FileObjects>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
