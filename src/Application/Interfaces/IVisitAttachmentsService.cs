using Application.DTOs.Files;

namespace Application.Interfaces;

public interface IVisitAttachmentsService
{
    Task<IEnumerable<VisitAttachmentDto>> GetByVisitIdAsync(long visitId);
    Task<VisitAttachmentDto> AttachAsync(VisitAttachmentDto dto);
    Task DetachAsync(long visitId, long fileId);
}
