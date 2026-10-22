using Domain.Entities;

namespace Domain.Interfaces;

public interface IVisitsRepository : ICrudRepository<Visits, long>
{
    Task<IEnumerable<Visits>> GetByPetIdAsync(long petId);
    Task ReplaceDiagnosesAsync(long visitId, IEnumerable<(int DiagnosisId, bool IsPrimary)> diagnoses);
}
