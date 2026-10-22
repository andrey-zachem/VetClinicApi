namespace Application.DTOs.Breeds;

public class BreedCreateDto
{
    public short SpeciesId { get; set; }
    public string Name { get; set; } = string.Empty;
}
