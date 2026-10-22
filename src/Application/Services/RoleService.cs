using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roles;

    public RoleService(IRoleRepository roles)
    {
        _roles = roles;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
        => (await _roles.GetAllAsync()).Select(Map);

    public async Task<RoleDto?> GetByIdAsync(short id)
    {
        var entity = await _roles.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<RoleDto> CreateAsync(RoleDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Role name is required");
        var entity = new Role { Name = dto.Name.Trim(), Description = dto.Description };
        await _roles.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(RoleDto dto)
    {
        var existing = Guard.Found(await _roles.GetByIdAsync(dto.Id), "Role");
        existing.Name = dto.Name;
        existing.Description = dto.Description;
        await _roles.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _roles.GetByIdAsync(id), "Role");
        await _roles.DeleteAsync(id);
    }

    private static RoleDto Map(Role r) => new() { Id = r.Id, Name = r.Name, Description = r.Description };
}
