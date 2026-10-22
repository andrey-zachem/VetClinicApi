namespace Domain.Entities;

public class Role
{
    public short Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<UserRole> UserRole { get; set; } = new List<UserRole>();
}
