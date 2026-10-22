using Domain.Entities;

namespace Application.DTOs.Users;

public class UserLogin
{
    public string Login { get; set; }
    public string Password { get; set; }
}