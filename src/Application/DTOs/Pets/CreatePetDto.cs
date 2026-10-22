namespace Application.DTOs.Pets;

public class CreatePetDto
{
    public long ClientId { get; set; }
    public int BreedId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sex { get; set; } = "unknown";
    public DateOnly? BirthDate { get; set; }
    public bool IsNeutered { get; set; }
    public string? Color { get; set; }
    public string? MicrochipNumber { get; set; }
    public long? PhotoFileId { get; set; }
    public DateOnly? DeceasedOn { get; set; }
}
