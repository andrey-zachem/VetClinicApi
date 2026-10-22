using Application.DTOs.Prescriptions;

namespace Application.Interfaces;

public interface IPrescriptionsService
{
    Task<IEnumerable<PrescriptionDto>> GetAllAsync();
    Task<PrescriptionDto?> GetByIdAsync(long id);
    Task<IEnumerable<PrescriptionDto>> GetByVisitIdAsync(long visitId);
    Task<PrescriptionDto> CreateAsync(PrescriptionDto dto);
    Task UpdateAsync(PrescriptionDto dto);
    Task DeleteAsync(long id);
}
