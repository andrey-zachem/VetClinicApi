using Domain.Enums;

namespace Domain.Entities;

public class Pets
{
    public long PetId { get; set; }
    public long ClientId { get; set; }
    public int BreedId { get; set; }
    public string Name { get; set; } = string.Empty;
    public PetSex Sex { get; set; } = PetSex.Unknown;
    public DateOnly? BirthDate { get; set; }
    public bool IsNeutered { get; set; }
    public string? Color { get; set; }
    public string? MicrochipNumber { get; set; }
    public long? PhotoFileId { get; set; }
    public DateOnly? DeceasedOn { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Clients Client { get; set; } = null!;
    public Breeds Breeds { get; set; } = null!;
    public FileObjects? Photo { get; set; }
    public ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
    public ICollection<Visits> Visits { get; set; } = new List<Visits>();
    public ICollection<Vaccinations> Vaccinations { get; set; } = new List<Vaccinations>();
}
