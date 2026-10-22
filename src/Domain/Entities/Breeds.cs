namespace Domain.Entities;

public class Breeds
{
    public int BreedsId { get; set; }
    public short SpeciesId { get; set; }
    public string Name { get; set; } = string.Empty;

    public Species Species { get; set; } = null!;
    public ICollection<Pets> Pets { get; set; } = new List<Pets>();
}
