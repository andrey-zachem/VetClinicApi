using Application.DTOs.Medications;

namespace Application.Interfaces;

public interface IMedicationsService
{
    Task<IEnumerable<MedicationDto>> GetAllAsync();
    Task<MedicationDto?> GetByIdAsync(int id);
    Task<MedicationDto> CreateAsync(MedicationDto dto);
    Task UpdateAsync(MedicationDto dto);
    Task DeleteAsync(int id);
}
