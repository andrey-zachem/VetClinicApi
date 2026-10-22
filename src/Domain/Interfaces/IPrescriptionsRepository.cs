using Domain.Entities;

namespace Domain.Interfaces;

public interface IPrescriptionsRepository : ICrudRepository<Prescriptions, long>
{
    Task<IEnumerable<Prescriptions>> GetByVisitIdAsync(long visitId);
}
