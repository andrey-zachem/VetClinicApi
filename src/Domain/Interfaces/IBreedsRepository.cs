using Domain.Entities;

namespace Domain.Interfaces;

public interface IBreedsRepository
{
    Task<Breeds?> GetByIdAsync(int id);
    Task<IEnumerable<Breeds>> GetAllAsync();
    Task AddAsync(Breeds breed);
    Task UpdateAsync(Breeds breed);
    Task DeleteAsync(int id);
}
