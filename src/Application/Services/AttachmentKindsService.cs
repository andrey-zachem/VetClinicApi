using Application.Common;
using Application.DTOs.Files;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AttachmentKindsService : IAttachmentKindsService
{
    private readonly ICrudRepository<AttachmentKinds, short> _kinds;

    public AttachmentKindsService(ICrudRepository<AttachmentKinds, short> kinds)
    {
        _kinds = kinds;
    }

    public async Task<IEnumerable<AttachmentKindDto>> GetAllAsync()
        => (await _kinds.GetAllAsync()).Select(Map);

    public async Task<AttachmentKindDto?> GetByIdAsync(short id)
    {
        var entity = await _kinds.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<AttachmentKindDto> CreateAsync(AttachmentKindDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Attachment kind name is required");

        var entity = new AttachmentKinds { Name = dto.Name.Trim() };
        await _kinds.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(AttachmentKindDto dto)
    {
        var existing = Guard.Found(await _kinds.GetByIdAsync(dto.KindId), "Attachment kind");
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Attachment kind name is required");

        existing.Name = dto.Name.Trim();
        await _kinds.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _kinds.GetByIdAsync(id), "Attachment kind");
        await _kinds.DeleteAsync(id);
    }

    private static AttachmentKindDto Map(AttachmentKinds kind) => new()
    {
        KindId = kind.KindId,
        Name = kind.Name
    };
}
