using Domain.Entities;

namespace Domain.Interfaces;

public interface IInvoicesRepository : ICrudRepository<Invoices, long>
{
    Task<IEnumerable<Invoices>> GetByClientIdAsync(long clientId);
    Task<IEnumerable<Invoices>> GetByVisitIdAsync(long visitId);
}
