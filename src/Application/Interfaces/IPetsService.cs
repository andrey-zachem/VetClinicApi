using Application.DTOs.Pets;

namespace Application.Interfaces;

public interface IPetService
{
    Task<PetsDao?> GetPetById(long id);
    Task<IEnumerable<PetsDao>> GetAllAsync();
    Task<IEnumerable<PetsDao>> GetByClientIdAsync(long clientId);
    Task<PetsDao> CreateAsync(CreatePetDto dto);
    Task UpdateAsync(UpdatePetDto dto);
    Task DeleteAsync(long id);
}
