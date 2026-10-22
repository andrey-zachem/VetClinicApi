namespace Application.DTOs.Files;

public class FileObjectCreateDto
{
    public string Bucket { get; set; } = string.Empty;
    public string ObjectKey { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeBytes { get; set; }
    public string? Etag { get; set; }
    public long? UploadedBy { get; set; }
}
