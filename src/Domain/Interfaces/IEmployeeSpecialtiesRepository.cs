using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployeeSpecialtiesRepository
{
    Task<EmployeeSpecialties?> GetByIdAsync(long employeeId, short specialtyId);
    Task<IEnumerable<EmployeeSpecialties>> GetAllAsync();
    Task<IEnumerable<EmployeeSpecialties>> GetByEmployeeIdAsync(long employeeId);
    Task AddAsync(EmployeeSpecialties employeeSpecialty);
    Task DeleteAsync(long employeeId, short specialtyId);
    Task ReplaceForEmployeeAsync(long employeeId, IEnumerable<short> specialtyIds);
}
