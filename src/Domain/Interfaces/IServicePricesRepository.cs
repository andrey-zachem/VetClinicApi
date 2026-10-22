using Domain.Entities;

namespace Domain.Interfaces;

public interface IServicePricesRepository : ICrudRepository<ServicePrices, long>
{
    Task<IEnumerable<ServicePrices>> GetByServiceIdAsync(int serviceId);
}
