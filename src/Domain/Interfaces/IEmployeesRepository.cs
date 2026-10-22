using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeesRepository
{
    Task<Employees?> GetByIdAsync(long id);
    Task<IEnumerable<Employees>> GetAllAsync();
    Task AddAsync(Employees employee);
    Task UpdateAsync(Employees employee);
    Task DeleteAsync(long id);
}