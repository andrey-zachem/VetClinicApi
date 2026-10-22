using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Users;

public class UserRegistration
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
