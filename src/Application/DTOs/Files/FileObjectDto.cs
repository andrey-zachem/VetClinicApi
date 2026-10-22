namespace Application.DTOs.Files;

public class FileObjectDto
{
    public long FileId { get; set; }
    public string Bucket { get; set; } = string.Empty;
    public string ObjectKey { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string? Etag { get; set; }
    public long? UploadedBy { get; set; }
    public DateTimeOffset UploadedAt { get; set; }
}
