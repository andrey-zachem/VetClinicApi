using Application.DTOs.Employees.Specialties;

namespace Application.Interfaces;

public interface ISpecialtiesService
{
    Task<IEnumerable<SpecialtiesDto>> GetAllSpecialtiesAsync();
    Task<SpecialtiesDto?> GetSpecialtyByIdAsync(short id);
    Task<SpecialtiesDto> CreateSpecialtyAsync(SpecialtiesDto specialty);
    Task UpdateSpecialtyAsync(SpecialtiesDto specialty);
    Task DeleteSpecialtyAsync(short id);
}
