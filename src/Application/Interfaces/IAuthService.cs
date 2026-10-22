using Application.DTOs.Users;
using Application.DTOs.Auth;    

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(UserLogin loginDto);
    Task<AuthResponseDto?> RegisterAsync(UserRegistration registerDto);
}