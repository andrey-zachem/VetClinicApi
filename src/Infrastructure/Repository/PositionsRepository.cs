using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PositionsRepository : IPositionsRepository
{
    private readonly AppDbContext _context;

    public PositionsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Positions?> GetByIdAsync(short id)
    {
        return await _context.Positions.FirstOrDefaultAsync(p => p.PositionId == id);
    }

    public async Task<IEnumerable<Positions>> GetAllAsync()
    {
        return await _context.Positions.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Positions position)
    {
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Positions position)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        var position = await _context.Positions.FindAsync(id);
        if (position != null)
        {
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }
    }
}
