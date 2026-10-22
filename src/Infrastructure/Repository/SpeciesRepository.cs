using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly AppDbContext _context;

    public SpeciesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Species?> GetByIdAsync(short id)
    {
        return await _context.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.SpeciesId == id);
    }

    public async Task<IEnumerable<Species>> GetAllAsync()
    {
        return await _context.Species.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Species species)
    {
        await _context.Species.AddAsync(species);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Species species)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        var species = await _context.Species.FindAsync(id);
        if (species != null)
        {
            _context.Species.Remove(species);
            await _context.SaveChangesAsync();
        }
    }
}
