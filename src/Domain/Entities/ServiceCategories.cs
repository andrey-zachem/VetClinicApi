namespace Domain.Entities;

public class ServiceCategories
{
    public short CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Services> Services { get; set; } = new List<Services>();
}
