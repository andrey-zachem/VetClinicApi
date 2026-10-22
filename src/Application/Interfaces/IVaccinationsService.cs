using Application.DTOs.Vaccines;

namespace Application.Interfaces;

public interface IVaccinationsService
{
    Task<IEnumerable<VaccinationDto>> GetAllAsync();
    Task<VaccinationDto?> GetByIdAsync(long id);
    Task<IEnumerable<VaccinationDto>> GetByPetIdAsync(long petId);
    Task<VaccinationDto> CreateAsync(VaccinationDto dto);
    Task UpdateAsync(VaccinationDto dto);
    Task DeleteAsync(long id);
}
