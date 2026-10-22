using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class WorkScheduleRepository : IWorkScheduleRepository
{
    private readonly AppDbContext _context;

    public WorkScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<WorkSchedule?> GetByIdAsync(long id)
    {
        return await _context.WorkSchedule.FirstOrDefaultAsync(ws => ws.ScheduleId == id);
    }

    public async Task<IEnumerable<WorkSchedule>> GetAllAsync()
    {
        return await _context.WorkSchedule.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<WorkSchedule>> GetByEmployeeIdAsync(long employeeId)
    {
        return await _context.WorkSchedule
            .AsNoTracking()
            .Where(ws => ws.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task AddAsync(WorkSchedule workSchedule)
    {
        await _context.WorkSchedule.AddAsync(workSchedule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkSchedule workSchedule)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var schedule = await _context.WorkSchedule.FindAsync(id);
        if (schedule != null)
        {
            _context.WorkSchedule.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
