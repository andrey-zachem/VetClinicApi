using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class VaccineBatchesRepository : CrudRepository<VaccineBatches, long>, IVaccineBatchesRepository
{
    public VaccineBatchesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<VaccineBatches>> GetByVaccineIdAsync(int vaccineId)
    {
        return await Set.AsNoTracking().Where(b => b.VaccineId == vaccineId).ToListAsync();
    }
}
