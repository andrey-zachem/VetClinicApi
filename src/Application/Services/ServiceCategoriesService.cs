using Application.Common;
using Application.DTOs.ClinicServices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ServiceCategoriesService : IServiceCategoriesService
{
    private readonly ICrudRepository<ServiceCategories, short> _categories;

    public ServiceCategoriesService(ICrudRepository<ServiceCategories, short> categories)
    {
        _categories = categories;
    }

    public async Task<IEnumerable<ServiceCategoryDto>> GetAllAsync()
        => (await _categories.GetAllAsync()).Select(Map);

    public async Task<ServiceCategoryDto?> GetByIdAsync(short id)
    {
        var entity = await _categories.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<ServiceCategoryDto> CreateAsync(ServiceCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Category name is required");
        var entity = new ServiceCategories { Name = dto.Name.Trim() };
        await _categories.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(ServiceCategoryDto dto)
    {
        var existing = Guard.Found(await _categories.GetByIdAsync(dto.CategoryId), "Service category");
        existing.Name = dto.Name;
        await _categories.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _categories.GetByIdAsync(id), "Service category");
        await _categories.DeleteAsync(id);
    }

    private static ServiceCategoryDto Map(ServiceCategories c) => new() { CategoryId = c.CategoryId, Name = c.Name };
}
