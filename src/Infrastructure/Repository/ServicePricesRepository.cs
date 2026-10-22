using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ServicePricesRepository : CrudRepository<ServicePrices, long>, IServicePricesRepository
{
    public ServicePricesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ServicePrices>> GetByServiceIdAsync(int serviceId)
    {
        return await Set.AsNoTracking().Where(p => p.ServiceId == serviceId).ToListAsync();
    }
}
