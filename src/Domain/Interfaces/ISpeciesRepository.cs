using Domain.Entities;

namespace Domain.Interfaces;

public interface ISpeciesRepository
{
    Task<Species?> GetByIdAsync(short id);
    Task<IEnumerable<Species>> GetAllAsync();
    Task AddAsync(Species species);
    Task UpdateAsync(Species species);
    Task DeleteAsync(short id);
}