using Domain.Entities;

namespace Domain.Interfaces;

public interface ISpecialtiesRepository
{
    Task<Specialties?> GetByIdAsync(short id);
    Task<IEnumerable<Specialties>> GetAllAsync();
    Task AddAsync(Specialties specialty);
    Task UpdateAsync(Specialties specialty);
    Task DeleteAsync(short id);
}