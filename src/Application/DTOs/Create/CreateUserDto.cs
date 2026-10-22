using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Create;

public class CreateUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? RoleId { get; set; }
}