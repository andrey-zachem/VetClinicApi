using Application.DTOs.Users;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Security;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        var authResponse = await _authService.LoginAsync(userLogin);
        if (authResponse is null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(authResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistration registerDto)
    {
        try
        {
            var authResponse = await _authService.RegisterAsync(registerDto);
            if (authResponse is null)
                return BadRequest();

            return Ok(authResponse);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
