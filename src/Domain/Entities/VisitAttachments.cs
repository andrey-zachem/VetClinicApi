namespace Domain.Entities;

public class VisitAttachments
{
    public long VisitId { get; set; }
    public long FileId { get; set; }
    public short KindId { get; set; }
    public string? Comment { get; set; }

    public Visits Visit { get; set; } = null!;
    public FileObjects File { get; set; } = null!;
    public AttachmentKinds Kind { get; set; } = null!;
}
