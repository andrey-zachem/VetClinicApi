using Application.DTOs.Employees.WorkSchedules;

namespace Application.Interfaces;

public interface IWorkScheduleService
{
    Task<IEnumerable<WorkScheduleDto>> GetAllAsync();
    Task<WorkScheduleDto?> GetByIdAsync(long id);
    Task<IEnumerable<WorkScheduleDto>> GetByEmployeeIdAsync(long employeeId);
    Task<WorkScheduleDto> CreateAsync(WorkScheduleCreate dto);
    Task UpdateAsync(WorkScheduleDto dto);
    Task DeleteAsync(long id);
}
