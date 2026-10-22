using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class InvoicesRepository : CrudRepository<Invoices, long>, IInvoicesRepository
{
    public InvoicesRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Invoices?> GetByIdAsync(long id)
    {
        return await Query().FirstOrDefaultAsync(i => i.InvoiceId == id);
    }

    public override async Task<IEnumerable<Invoices>> GetAllAsync()
    {
        return await Query().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Invoices>> GetByClientIdAsync(long clientId)
    {
        return await Query().AsNoTracking().Where(i => i.ClientId == clientId).ToListAsync();
    }

    public async Task<IEnumerable<Invoices>> GetByVisitIdAsync(long visitId)
    {
        return await Query().AsNoTracking().Where(i => i.VisitId == visitId).ToListAsync();
    }

    private IQueryable<Invoices> Query()
    {
        return Set
            .Include(i => i.Items)
            .Include(i => i.Payments);
    }
}
