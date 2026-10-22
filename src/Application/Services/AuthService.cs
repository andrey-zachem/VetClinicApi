using Application.DTOs.Auth;
using Application.DTOs.Users;
using Application.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IJwtService jwtService,
        IUserService userService,
        IPasswordHasher passwordHasher)
    {
        _jwtService = jwtService;
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto?> RegisterAsync(UserRegistration registrationDto)
    {
        var createdUser = await _userService.RegisterUserAsync(registrationDto);
        if (createdUser == null)
            return null;

        var role = string.IsNullOrEmpty(createdUser.Role) ? "client" : createdUser.Role;

        var token = _jwtService.GenerateToken(
            createdUser.Id,
            createdUser.Login,
            role);

        return new AuthResponseDto
        {
            Token = token
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(UserLogin userLogin)
    {
        var user = await _userService.GetByLoginAsync(userLogin.Login);
        if (user == null)
            return null;

        if (!user.IsActive)
            return null;

        var validPassword = _passwordHasher.VerifyPassword(
            userLogin.Password,
            user.PasswordHash);

        if (!validPassword)
            return null;

        var token = _jwtService.GenerateToken(
            user.Id,
            user.Login,
            user.Role);

        return new AuthResponseDto
        {
            Token = token
        };
    }
}
