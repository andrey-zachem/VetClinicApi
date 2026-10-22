using Application.DTOs.Visits;

namespace Application.Interfaces;

public interface IVisitsService
{
    Task<IEnumerable<VisitDto>> GetAllAsync();
    Task<VisitDto?> GetByIdAsync(long id);
    Task<IEnumerable<VisitDto>> GetByPetIdAsync(long petId);
    Task<VisitDto> CreateAsync(VisitCreateDto dto);
    Task UpdateAsync(VisitDto dto);
    Task DeleteAsync(long id);
}
