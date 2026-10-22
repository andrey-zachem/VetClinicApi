using Application.DTOs;

namespace Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync();
    Task<RoleDto?> GetByIdAsync(short id);
    Task<RoleDto> CreateAsync(RoleDto dto);
    Task UpdateAsync(RoleDto dto);
    Task DeleteAsync(short id);
}
