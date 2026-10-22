using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : ICrudRepository<User, long>
{
    Task<User?> GetByLoginAsync(string login);
    Task<User> CreateAsync(User user);
}
