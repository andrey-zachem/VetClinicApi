using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly AppDbContext _context;

    public EmployeesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employees?> GetByIdAsync(long id)
    {
        return await Query()
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<IEnumerable<Employees>> GetAllAsync()
    {
        return await Query()
            .AsNoTracking()
            .ToListAsync();
    }

    private IQueryable<Employees> Query() =>
        _context.Employees
            .AsSplitQuery()
            .Include(e => e.Position)
            .Include(e => e.EmployeeSpecialties);

    public async Task AddAsync(Employees employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employees employee)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
