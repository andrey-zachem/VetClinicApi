namespace Application.DTOs.Files;

public class VisitAttachmentDto
{
    public long VisitId { get; set; }
    public long FileId { get; set; }
    public short KindId { get; set; }
    public string? Comment { get; set; }
}
