using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly AppDbContext _context;

    public AuditLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AuditLog?> GetByIdAsync(long id)
    {
        return await _context.AuditLog.FirstOrDefaultAsync(a => a.AuditId == id);
    }

    public async Task<IEnumerable<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLog.AsNoTracking().OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<AuditLog>> GetByTableAsync(string tableName)
    {
        return await _context.AuditLog
            .AsNoTracking()
            .Where(a => a.TableName == tableName)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }
}
