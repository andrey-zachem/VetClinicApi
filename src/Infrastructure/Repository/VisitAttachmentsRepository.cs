using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class VisitAttachmentsRepository : IVisitAttachmentsRepository
{
    private readonly AppDbContext _context;

    public VisitAttachmentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<VisitAttachments?> GetByIdAsync(long visitId, long fileId)
    {
        return await _context.VisitAttachments
            .FirstOrDefaultAsync(a => a.VisitId == visitId && a.FileId == fileId);
    }

    public async Task<IEnumerable<VisitAttachments>> GetByVisitIdAsync(long visitId)
    {
        return await _context.VisitAttachments
            .AsNoTracking()
            .Where(a => a.VisitId == visitId)
            .ToListAsync();
    }

    public async Task AddAsync(VisitAttachments attachment)
    {
        await _context.VisitAttachments.AddAsync(attachment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long visitId, long fileId)
    {
        var existing = await GetByIdAsync(visitId, fileId);
        if (existing is null)
            return;

        _context.VisitAttachments.Remove(existing);
        await _context.SaveChangesAsync();
    }
}
