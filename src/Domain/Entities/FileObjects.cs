namespace Domain.Entities;

public class FileObjects
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

    public User? UploadedByUser { get; set; }
    public ICollection<Pets> Pets { get; set; } = new List<Pets>();
    public ICollection<VisitAttachments> VisitAttachments { get; set; } = new List<VisitAttachments>();
}
