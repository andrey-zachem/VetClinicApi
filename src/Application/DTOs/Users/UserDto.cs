namespace Application.DTOs.Users;

public class UserDto
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Role { get; set; }
}
