using Application.DTOs.Vaccines;

namespace Application.Interfaces;

public interface IVaccineBatchesService
{
    Task<IEnumerable<VaccineBatchDto>> GetAllAsync();
    Task<VaccineBatchDto?> GetByIdAsync(long id);
    Task<IEnumerable<VaccineBatchDto>> GetByVaccineIdAsync(int vaccineId);
    Task<VaccineBatchDto> CreateAsync(VaccineBatchDto dto);
    Task UpdateAsync(VaccineBatchDto dto);
    Task DeleteAsync(long id);
}
