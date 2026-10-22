using Application.DTOs.Vaccines;

namespace Application.Interfaces;

public interface IVaccinesService
{
    Task<IEnumerable<VaccineDto>> GetAllAsync();
    Task<VaccineDto?> GetByIdAsync(int id);
    Task<VaccineDto> CreateAsync(VaccineDto dto);
    Task UpdateAsync(VaccineDto dto);
    Task DeleteAsync(int id);
}
