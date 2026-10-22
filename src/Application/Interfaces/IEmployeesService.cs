using Application.DTOs.Employees;

namespace Application.Interfaces;

public interface IEmployeesService
{
    Task<IEnumerable<EmployeesDto>> GetAllEmployeesAsync();
    Task<EmployeesDto?> GetEmployeeByIdAsync(long id);
    Task<EmployeesDto> CreateEmployeeAsync(EmployeesCreate employee);
    Task UpdateEmployeeAsync(EmployeesDto employee);
    Task DeleteEmployeeAsync(long id);
}
