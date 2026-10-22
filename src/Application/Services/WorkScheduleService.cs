using Application.DTOs.Employees.WorkSchedules;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class WorkScheduleService : IWorkScheduleService
{
    private readonly IWorkScheduleRepository _workScheduleRepository;
    private readonly IEmployeesRepository _employeesRepository;

    public WorkScheduleService(
        IWorkScheduleRepository workScheduleRepository,
        IEmployeesRepository employeesRepository)
    {
        _workScheduleRepository = workScheduleRepository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<WorkScheduleDto>> GetAllAsync()
    {
        var schedules = await _workScheduleRepository.GetAllAsync();
        return schedules.Select(MapToDto);
    }

    public async Task<WorkScheduleDto?> GetByIdAsync(long id)
    {
        var schedule = await _workScheduleRepository.GetByIdAsync(id);
        return schedule is null ? null : MapToDto(schedule);
    }

    public async Task<IEnumerable<WorkScheduleDto>> GetByEmployeeIdAsync(long employeeId)
    {
        var schedules = await _workScheduleRepository.GetByEmployeeIdAsync(employeeId);
        return schedules.Select(MapToDto);
    }

    public async Task<WorkScheduleDto> CreateAsync(WorkScheduleCreate dto)
    {
        ValidateSchedule(dto.WeekDay, dto.StartsAt, dto.EndsAt);
        await EnsureEmployeeExists(dto.EmployeeId);

        var entity = new WorkSchedule
        {
            EmployeeId = dto.EmployeeId,
            WeekDay = dto.WeekDay,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt
        };

        await _workScheduleRepository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task UpdateAsync(WorkScheduleDto dto)
    {
        ValidateSchedule(dto.WeekDay, dto.StartsAt, dto.EndsAt);

        var existing = await _workScheduleRepository.GetByIdAsync(dto.ScheduleId);
        if (existing == null)
            throw new KeyNotFoundException("Work schedule not found");

        await EnsureEmployeeExists(dto.EmployeeId);

        existing.EmployeeId = dto.EmployeeId;
        existing.WeekDay = dto.WeekDay;
        existing.StartsAt = dto.StartsAt;
        existing.EndsAt = dto.EndsAt;
        await _workScheduleRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _workScheduleRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Work schedule not found");

        await _workScheduleRepository.DeleteAsync(id);
    }

    private async Task EnsureEmployeeExists(long employeeId)
    {
        var employee = await _employeesRepository.GetByIdAsync(employeeId);
        if (employee == null)
            throw new KeyNotFoundException($"Employee {employeeId} not found");
    }

    private static void ValidateSchedule(short weekDay, TimeOnly startsAt, TimeOnly endsAt)
    {
        if (weekDay is < 1 or > 7)
            throw new ArgumentException("WeekDay must be in range 1..7");
        if (endsAt <= startsAt)
            throw new ArgumentException("EndsAt must be later than StartsAt");
    }

    private static WorkScheduleDto MapToDto(WorkSchedule schedule) => new()
    {
        ScheduleId = schedule.ScheduleId,
        EmployeeId = schedule.EmployeeId,
        WeekDay = schedule.WeekDay,
        StartsAt = schedule.StartsAt,
        EndsAt = schedule.EndsAt
    };
}
