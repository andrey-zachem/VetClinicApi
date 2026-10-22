using Domain.Entities;

namespace Domain.Interfaces;

public interface IPetsRepository : ICrudRepository<Pets, long>
{
    Task<Pets?> GetPetByIdAsync(long petId);
    Task<IEnumerable<Pets>> GetByClientIdAsync(long clientId);
}
