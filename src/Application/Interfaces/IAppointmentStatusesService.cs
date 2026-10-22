using Application.DTOs.Appointments;

namespace Application.Interfaces;

public interface IAppointmentStatusesService
{
    Task<IEnumerable<AppointmentStatusDto>> GetAllAsync();
    Task<AppointmentStatusDto?> GetByIdAsync(short id);
    Task<AppointmentStatusDto> CreateAsync(AppointmentStatusDto dto);
    Task UpdateAsync(AppointmentStatusDto dto);
    Task DeleteAsync(short id);
}
