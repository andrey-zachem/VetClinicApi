using Domain.Entities;

namespace Domain.Interfaces;

public interface IVaccinationsRepository : ICrudRepository<Vaccinations, long>
{
    Task<IEnumerable<Vaccinations>> GetByPetIdAsync(long petId);
}
