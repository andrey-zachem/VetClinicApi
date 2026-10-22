using Application.DTOs.Files;

namespace Application.Interfaces;

public interface IFileObjectsService
{
    Task<IEnumerable<FileObjectDto>> GetAllAsync();
    Task<FileObjectDto?> GetByIdAsync(long id);
    Task<FileObjectDto> CreateAsync(FileObjectCreateDto dto);
    Task DeleteAsync(long id);
}
