using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class BreedsRepository : IBreedsRepository
{
    private readonly AppDbContext _context;

    public BreedsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Breeds?> GetByIdAsync(int id)
    {
        return await Query()
            .FirstOrDefaultAsync(b => b.BreedsId == id);
    }

    public async Task<IEnumerable<Breeds>> GetAllAsync()
    {
        return await Query()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Breeds breed)
    {
        await _context.Breeds.AddAsync(breed);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Breeds breed)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var breed = await _context.Breeds.FindAsync(id);
        if (breed != null)
        {
            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();
        }
    }

    private IQueryable<Breeds> Query() =>
        _context.Breeds.Include(b => b.Species);
}
