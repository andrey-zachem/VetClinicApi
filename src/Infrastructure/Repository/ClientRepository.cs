using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ClientRepository : CrudRepository<Clients, long>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Clients?> GetByIdAsync(long id)
    {
        return await Set
            .Include(c => c.Pets)
            .FirstOrDefaultAsync(c => c.ClientId == id);
    }

    public override async Task<IEnumerable<Clients>> GetAllAsync()
    {
        return await Set
            .AsNoTracking()
            .Include(c => c.Pets)
            .ToListAsync();
    }
}
