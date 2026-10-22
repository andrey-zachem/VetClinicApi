namespace Domain.Entities;

public class Species
{
    public short SpeciesId { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Breeds> Breeds { get; set; } = new List<Breeds>();
}
