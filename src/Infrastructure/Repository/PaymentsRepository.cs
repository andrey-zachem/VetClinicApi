using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PaymentsRepository : CrudRepository<Payments, long>, IPaymentsRepository
{
    public PaymentsRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payments>> GetByInvoiceIdAsync(long invoiceId)
    {
        return await Set.AsNoTracking().Where(p => p.InvoiceId == invoiceId).ToListAsync();
    }
}
