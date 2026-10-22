using Application.DTOs.Species;

namespace Application.DTOs.Breeds;

public class BreedsDao
{
    public int BreedId { get; set; }
    public short SpeciesId { get; set; }
    public string Name { get; set; } = string.Empty;
    public SpeciesDao? Species { get; set; }
}
