using Domain.Entities;

namespace Domain.Interfaces;

public interface IPaymentsRepository : ICrudRepository<Payments, long>
{
    Task<IEnumerable<Payments>> GetByInvoiceIdAsync(long invoiceId);
}
