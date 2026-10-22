using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class VaccinationsRepository : CrudRepository<Vaccinations, long>, IVaccinationsRepository
{
    public VaccinationsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Vaccinations>> GetByPetIdAsync(long petId)
    {
        return await Set.AsNoTracking().Where(v => v.PetId == petId).ToListAsync();
    }
}
