using Application.DTOs.Create;
using Application.DTOs.Users;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is null ? null : Map(user);
    }

    public async Task<UserAuthRecord?> GetByLoginAsync(string login)
    {
        var user = await _userRepository.GetByLoginAsync(login);
        if (user == null)
            return null;

        return new UserAuthRecord
        {
            Id = user.Id,
            Login = user.Login,
            IsActive = user.IsActive,
            PasswordHash = user.PasswordHash,
            Role = ResolveRole(user) ?? "client"
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(Map);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto userDto)
    {
        var passwordHash = _passwordHasher.HashPassword(userDto.Password);
        var newUser = new User
        {
            Login = userDto.Login,
            PasswordHash = passwordHash,
        };

        if (userDto.RoleId is int roleId)
            await AttachRoleAsync(newUser, (short)roleId);

        await _userRepository.CreateAsync(newUser);
        var created = await _userRepository.GetByIdAsync(newUser.Id);
        return Map(created ?? newUser);
    }

    public async Task UpdateAsync(UserDto userDto)
    {
        var user = await _userRepository.GetByIdAsync(userDto.Id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userDto.Id} not found.");

        user.Login = userDto.Login;
        user.IsActive = userDto.IsActive;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {id} not found.");
        await _userRepository.DeleteAsync(id);
    }

    public async Task<UserDto?> RegisterUserAsync(UserRegistration registrationDto)
    {
        var existingUser = await _userRepository.GetByLoginAsync(registrationDto.Login);
        if (existingUser != null)
            throw new ArgumentException("User with this login already exists.");

        var passwordHash = _passwordHasher.HashPassword(registrationDto.Password);
        var newUser = new User
        {
            Login = registrationDto.Login,
            PasswordHash = passwordHash,
            IsActive = true
        };

        await AttachRoleByNameAsync(newUser, "client");
        await _userRepository.CreateAsync(newUser);

        var created = await _userRepository.GetByIdAsync(newUser.Id);
        return created is null ? null : Map(created);
    }

    public async Task<UserDto?> LoginUser(UserRegistration registrationDto)
    {
        var user = await _userRepository.GetByLoginAsync(registrationDto.Login);
        if (user == null || !_passwordHasher.VerifyPassword(registrationDto.Password, user.PasswordHash))
            return null;

        return Map(user);
    }

    private async Task AttachRoleAsync(User user, short roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            throw new KeyNotFoundException($"Role {roleId} not found.");

        user.UserRole.Add(new UserRole
        {
            RoleId = role.Id,
            Role = role
        });
    }

    private async Task AttachRoleByNameAsync(User user, string roleName)
    {
        var roles = await _roleRepository.GetAllAsync();
        var role = roles.FirstOrDefault(r =>
            string.Equals(r.Name, roleName, StringComparison.OrdinalIgnoreCase));
        if (role == null)
            return;

        user.UserRole.Add(new UserRole
        {
            RoleId = role.Id,
            Role = role
        });
    }

    private static string? ResolveRole(User user) =>
        user.UserRole?
            .Select(ur => ur.Role?.Name)
            .FirstOrDefault(name => !string.IsNullOrWhiteSpace(name));

    private static UserDto Map(User user) => new()
    {
        Id = user.Id,
        Login = user.Login,
        IsActive = user.IsActive,
        Role = ResolveRole(user)
    };
}
