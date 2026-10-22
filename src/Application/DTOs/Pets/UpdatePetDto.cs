namespace Application.DTOs.Pets;

public class UpdatePetDto : CreatePetDto
{
    public long PetId { get; set; }
}
