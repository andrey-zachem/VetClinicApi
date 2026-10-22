using Application.Common;
using Application.DTOs.Files;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class VisitAttachmentsService : IVisitAttachmentsService
{
    private readonly IVisitAttachmentsRepository _attachments;
    private readonly IVisitsRepository _visits;
    private readonly ICrudRepository<FileObjects, long> _files;
    private readonly ICrudRepository<AttachmentKinds, short> _kinds;

    public VisitAttachmentsService(
        IVisitAttachmentsRepository attachments,
        IVisitsRepository visits,
        ICrudRepository<FileObjects, long> files,
        ICrudRepository<AttachmentKinds, short> kinds)
    {
        _attachments = attachments;
        _visits = visits;
        _files = files;
        _kinds = kinds;
    }

    public async Task<IEnumerable<VisitAttachmentDto>> GetByVisitIdAsync(long visitId)
        => (await _attachments.GetByVisitIdAsync(visitId)).Select(Map);

    public async Task<VisitAttachmentDto> AttachAsync(VisitAttachmentDto dto)
    {
        Guard.Found(await _visits.GetByIdAsync(dto.VisitId), $"Visit {dto.VisitId}");
        Guard.Found(await _files.GetByIdAsync(dto.FileId), $"File {dto.FileId}");
        Guard.Found(await _kinds.GetByIdAsync(dto.KindId), $"Attachment kind {dto.KindId}");

        if (await _attachments.GetByIdAsync(dto.VisitId, dto.FileId) is not null)
            throw new ArgumentException("Attachment already exists");

        var entity = new VisitAttachments
        {
            VisitId = dto.VisitId,
            FileId = dto.FileId,
            KindId = dto.KindId,
            Comment = dto.Comment
        };
        await _attachments.AddAsync(entity);
        return Map(entity);
    }

    public async Task DetachAsync(long visitId, long fileId)
    {
        Guard.Found(await _attachments.GetByIdAsync(visitId, fileId), "Visit attachment");
        await _attachments.DeleteAsync(visitId, fileId);
    }

    private static VisitAttachmentDto Map(VisitAttachments attachment) => new()
    {
        VisitId = attachment.VisitId,
        FileId = attachment.FileId,
        KindId = attachment.KindId,
        Comment = attachment.Comment
    };
}
