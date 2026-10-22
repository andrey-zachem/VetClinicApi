using Application.DTOs.Employees.Specialties;

namespace Application.Interfaces;

public interface IEmployeeSpecialtiesService
{
    Task<IEnumerable<EmployeeSpecialtyDto>> GetAllAsync();
    Task<IEnumerable<EmployeeSpecialtyDto>> GetByEmployeeIdAsync(long employeeId);
    Task<EmployeeSpecialtyDto> AssignAsync(EmployeeSpecialtyDto dto);
    Task UnassignAsync(long employeeId, short specialtyId);
}
