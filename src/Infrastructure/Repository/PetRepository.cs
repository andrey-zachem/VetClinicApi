using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PetRepository : CrudRepository<Pets, long>, IPetsRepository
{
    public PetRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Pets?> GetByIdAsync(long id)
    {
        return await Query()
            .FirstOrDefaultAsync(p => p.PetId == id);
    }

    public Task<Pets?> GetPetByIdAsync(long petId) => GetByIdAsync(petId);

    public override async Task<IEnumerable<Pets>> GetAllAsync()
    {
        return await Query()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Pets>> GetByClientIdAsync(long clientId)
    {
        return await Query()
            .AsNoTracking()
            .Where(p => p.ClientId == clientId)
            .ToListAsync();
    }

    private IQueryable<Pets> Query()
    {
        return Set
            .AsSplitQuery()
            .Include(p => p.Client)
            .Include(p => p.Breeds)
                .ThenInclude(b => b.Species);
    }
}
