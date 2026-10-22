using Domain.Entities;

namespace Domain.Interfaces;

public interface IWorkScheduleRepository
{
    Task<WorkSchedule?> GetByIdAsync(long id);
    Task<IEnumerable<WorkSchedule>> GetAllAsync();
    Task<IEnumerable<WorkSchedule>> GetByEmployeeIdAsync(long employeeId);
    Task AddAsync(WorkSchedule workSchedule);
    Task UpdateAsync(WorkSchedule workSchedule);
    Task DeleteAsync(long id);
}
