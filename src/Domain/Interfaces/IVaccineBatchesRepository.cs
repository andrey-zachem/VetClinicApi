using Domain.Entities;

namespace Domain.Interfaces;

public interface IVaccineBatchesRepository : ICrudRepository<VaccineBatches, long>
{
    Task<IEnumerable<VaccineBatches>> GetByVaccineIdAsync(int vaccineId);
}
