using Domain.Entities;

namespace Domain.Interfaces;

public interface IVisitAttachmentsRepository
{
    Task<VisitAttachments?> GetByIdAsync(long visitId, long fileId);
    Task<IEnumerable<VisitAttachments>> GetByVisitIdAsync(long visitId);
    Task AddAsync(VisitAttachments attachment);
    Task DeleteAsync(long visitId, long fileId);
}
