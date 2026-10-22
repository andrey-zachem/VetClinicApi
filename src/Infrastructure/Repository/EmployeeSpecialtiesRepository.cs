using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class EmployeeSpecialtiesRepository : IEmployeeSpecialtiesRepository
{
    private readonly AppDbContext _context;

    public EmployeeSpecialtiesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeSpecialties?> GetByIdAsync(long employeeId, short specialtyId)
    {
        return await _context.EmployeeSpecialties
            .FirstOrDefaultAsync(es => es.EmployeeId == employeeId && es.SpecialtyId == specialtyId);
    }

    public async Task<IEnumerable<EmployeeSpecialties>> GetAllAsync()
    {
        return await _context.EmployeeSpecialties.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<EmployeeSpecialties>> GetByEmployeeIdAsync(long employeeId)
    {
        return await _context.EmployeeSpecialties
            .AsNoTracking()
            .Where(es => es.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task AddAsync(EmployeeSpecialties employeeSpecialty)
    {
        await _context.EmployeeSpecialties.AddAsync(employeeSpecialty);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long employeeId, short specialtyId)
    {
        var existing = await GetByIdAsync(employeeId, specialtyId);
        if (existing != null)
        {
            _context.EmployeeSpecialties.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ReplaceForEmployeeAsync(long employeeId, IEnumerable<short> specialtyIds)
    {
        var current = await _context.EmployeeSpecialties
            .Where(es => es.EmployeeId == employeeId)
            .ToListAsync();

        _context.EmployeeSpecialties.RemoveRange(current);

        var uniqueIds = specialtyIds.Distinct().ToList();
        foreach (var specialtyId in uniqueIds)
        {
            await _context.EmployeeSpecialties.AddAsync(new EmployeeSpecialties
            {
                EmployeeId = employeeId,
                SpecialtyId = specialtyId
            });
        }

        await _context.SaveChangesAsync();
    }
}
