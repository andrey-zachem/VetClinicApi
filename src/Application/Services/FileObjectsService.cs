using Application.Common;
using Application.DTOs.Files;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class FileObjectsService : IFileObjectsService
{
    private readonly ICrudRepository<FileObjects, long> _files;
    private readonly IUserRepository _users;

    public FileObjectsService(ICrudRepository<FileObjects, long> files, IUserRepository users)
    {
        _files = files;
        _users = users;
    }

    public async Task<IEnumerable<FileObjectDto>> GetAllAsync()
        => (await _files.GetAllAsync()).Select(Map);

    public async Task<FileObjectDto?> GetByIdAsync(long id)
    {
        var entity = await _files.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<FileObjectDto> CreateAsync(FileObjectCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Bucket) || string.IsNullOrWhiteSpace(dto.ObjectKey))
            throw new ArgumentException("Bucket and object key are required");
        if (string.IsNullOrWhiteSpace(dto.OriginalName))
            throw new ArgumentException("Original name is required");
        if (dto.SizeBytes < 0)
            throw new ArgumentException("Size cannot be negative");
        if (dto.UploadedBy is not null)
            Guard.Found(await _users.GetByIdAsync(dto.UploadedBy.Value), $"User {dto.UploadedBy}");

        var entity = new FileObjects
        {
            Bucket = dto.Bucket.Trim(),
            ObjectKey = dto.ObjectKey.Trim(),
            OriginalName = dto.OriginalName.Trim(),
            ContentType = dto.ContentType.Trim(),
            SizeBytes = dto.SizeBytes,
            Etag = dto.Etag,
            UploadedBy = dto.UploadedBy,
            UploadedAt = DateTimeOffset.UtcNow
        };
        await _files.AddAsync(entity);
        return Map(entity);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _files.GetByIdAsync(id), "File");
        await _files.DeleteAsync(id);
    }

    private static FileObjectDto Map(FileObjects file) => new()
    {
        FileId = file.FileId,
        Bucket = file.Bucket,
        ObjectKey = file.ObjectKey,
        OriginalName = file.OriginalName,
        ContentType = file.ContentType,
        SizeBytes = file.SizeBytes,
        Etag = file.Etag,
        UploadedBy = file.UploadedBy,
        UploadedAt = file.UploadedAt
    };
}
