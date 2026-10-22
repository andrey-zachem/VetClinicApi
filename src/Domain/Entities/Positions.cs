namespace Domain.Entities;

public class Positions
{
    public short PositionId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Employees> Employees { get; set; } = new List<Employees>();
}
