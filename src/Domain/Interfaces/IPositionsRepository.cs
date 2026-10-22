using Domain.Entities;

namespace Domain.Interfaces;

public interface IPositionsRepository
{
    Task<Positions?> GetByIdAsync(short id);
    Task<IEnumerable<Positions>> GetAllAsync();
    Task AddAsync(Positions position);
    Task UpdateAsync(Positions position);
    Task DeleteAsync(short id);
}
