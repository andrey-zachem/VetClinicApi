using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class UserRepository : CrudRepository<User, long>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<User?> GetByIdAsync(long id)
    {
        return await Set
            .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await Set
            .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Login == login);
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await Set
            .AsNoTracking()
            .Include(u => u.UserRole)
                .ThenInclude(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        await AddAsync(user);
        return user;
    }
}
