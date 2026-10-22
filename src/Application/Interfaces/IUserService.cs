using Application.DTOs.Users;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(long id);
    Task<UserAuthRecord?> GetByLoginAsync(string login);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> RegisterUserAsync(UserRegistration registrationDto);
    Task<UserDto?> LoginUser(UserRegistration registrationDto);
    Task UpdateAsync(UserDto user);
    Task DeleteAsync(long id);
}
