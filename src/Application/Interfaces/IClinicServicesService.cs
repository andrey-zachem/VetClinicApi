using Application.DTOs.ClinicServices;

namespace Application.Interfaces;

public interface IClinicServicesService
{
    Task<IEnumerable<ClinicServiceDto>> GetAllAsync();
    Task<ClinicServiceDto?> GetByIdAsync(int id);
    Task<ClinicServiceDto> CreateAsync(ClinicServiceDto dto);
    Task UpdateAsync(ClinicServiceDto dto);
    Task DeleteAsync(int id);
}
