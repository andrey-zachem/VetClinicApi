using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByIdAsync(short id)
    {
        return await _context.Role.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Role.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Role role)
    {
        await _context.Role.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role role)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        var role = await _context.Role.FindAsync(id);
        if (role != null)
        {
            _context.Role.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
