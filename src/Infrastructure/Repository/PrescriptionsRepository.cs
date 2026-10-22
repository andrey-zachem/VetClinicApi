using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PrescriptionsRepository : CrudRepository<Prescriptions, long>, IPrescriptionsRepository
{
    public PrescriptionsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Prescriptions>> GetByVisitIdAsync(long visitId)
    {
        return await Set.AsNoTracking().Where(p => p.VisitId == visitId).ToListAsync();
    }
}
