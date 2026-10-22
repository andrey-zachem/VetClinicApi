using Application.DTOs.Diagnoses;

namespace Application.Interfaces;

public interface IDiagnosesService
{
    Task<IEnumerable<DiagnosisDto>> GetAllAsync();
    Task<DiagnosisDto?> GetByIdAsync(int id);
    Task<DiagnosisDto> CreateAsync(DiagnosisDto dto);
    Task UpdateAsync(DiagnosisDto dto);
    Task DeleteAsync(int id);
}
