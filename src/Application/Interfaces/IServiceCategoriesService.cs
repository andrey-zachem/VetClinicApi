using Application.DTOs.ClinicServices;

namespace Application.Interfaces;

public interface IServiceCategoriesService
{
    Task<IEnumerable<ServiceCategoryDto>> GetAllAsync();
    Task<ServiceCategoryDto?> GetByIdAsync(short id);
    Task<ServiceCategoryDto> CreateAsync(ServiceCategoryDto dto);
    Task UpdateAsync(ServiceCategoryDto dto);
    Task DeleteAsync(short id);
}
