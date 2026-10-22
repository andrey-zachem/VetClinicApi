using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
}