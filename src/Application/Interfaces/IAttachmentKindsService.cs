using Application.DTOs.Files;

namespace Application.Interfaces;

public interface IAttachmentKindsService
{
    Task<IEnumerable<AttachmentKindDto>> GetAllAsync();
    Task<AttachmentKindDto?> GetByIdAsync(short id);
    Task<AttachmentKindDto> CreateAsync(AttachmentKindDto dto);
    Task UpdateAsync(AttachmentKindDto dto);
    Task DeleteAsync(short id);
}
