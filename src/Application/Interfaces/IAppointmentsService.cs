using Application.DTOs.Appointments;

namespace Application.Interfaces;

public interface IAppointmentsService
{
    Task<IEnumerable<AppointmentDto>> GetAllAsync();
    Task<AppointmentDto?> GetByIdAsync(long id);
    Task<IEnumerable<AppointmentDto>> GetByPetIdAsync(long petId);
    Task<IEnumerable<AppointmentDto>> GetByEmployeeIdAsync(long employeeId);
    Task<AppointmentDto> CreateAsync(AppointmentCreateDto dto);
    Task UpdateAsync(AppointmentDto dto);
    Task DeleteAsync(long id);
}
