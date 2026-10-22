namespace Domain.Entities;

public class AttachmentKinds
{
    public short KindId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<VisitAttachments> VisitAttachments { get; set; } = new List<VisitAttachments>();
}
