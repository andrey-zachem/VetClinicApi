namespace Application.DTOs.Users;

/// <summary>
/// Internal auth payload — never expose via API controllers.
/// </summary>
public class UserAuthRecord
{
    public long Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Role { get; set; } = "client";
}
