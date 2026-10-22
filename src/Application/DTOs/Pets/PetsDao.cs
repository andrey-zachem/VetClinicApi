using Application.DTOs.Breeds;
using Application.DTOs.Client;

namespace Application.DTOs.Pets;

public class PetsDao
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public int BreedId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public bool IsNeutered { get; set; }
    public string? Color { get; set; }
    public string? MicrochipNumber { get; set; }
    public long? PhotoFileId { get; set; }
    public DateOnly? DeceasedOn { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public ClientDto? Client { get; set; }
    public BreedsDao? Breed { get; set; }
}
