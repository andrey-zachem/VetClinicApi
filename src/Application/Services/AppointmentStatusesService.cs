using Application.Common;
using Application.DTOs.Appointments;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AppointmentStatusesService : IAppointmentStatusesService
{
    private readonly ICrudRepository<AppointmentStatuses, short> _statuses;

    public AppointmentStatusesService(ICrudRepository<AppointmentStatuses, short> statuses)
    {
        _statuses = statuses;
    }

    public async Task<IEnumerable<AppointmentStatusDto>> GetAllAsync()
        => (await _statuses.GetAllAsync()).Select(Map);

    public async Task<AppointmentStatusDto?> GetByIdAsync(short id)
    {
        var entity = await _statuses.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<AppointmentStatusDto> CreateAsync(AppointmentStatusDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Status name is required");
        var entity = new AppointmentStatuses { Name = dto.Name.Trim() };
        await _statuses.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(AppointmentStatusDto dto)
    {
        var existing = Guard.Found(await _statuses.GetByIdAsync(dto.StatusId), "Appointment status");
        existing.Name = dto.Name;
        await _statuses.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _statuses.GetByIdAsync(id), "Appointment status");
        await _statuses.DeleteAsync(id);
    }

    private static AppointmentStatusDto Map(AppointmentStatuses s) => new() { StatusId = s.StatusId, Name = s.Name };
}
