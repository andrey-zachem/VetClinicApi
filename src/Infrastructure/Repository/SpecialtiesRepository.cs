using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SpecialtiesRepository : ISpecialtiesRepository
{
    private readonly AppDbContext _context;

    public SpecialtiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Specialties?> GetByIdAsync(short id)
    {
        return await _context.Specialties.FirstOrDefaultAsync(s => s.SpecialtyId == id);
    }

    public async Task<IEnumerable<Specialties>> GetAllAsync()
    {
        return await _context.Specialties.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Specialties specialty)
    {
        await _context.Specialties.AddAsync(specialty);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Specialties specialty)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(short id)
    {
        var specialty = await _context.Specialties.FindAsync(id);
        if (specialty != null)
        {
            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
        }
    }
}
