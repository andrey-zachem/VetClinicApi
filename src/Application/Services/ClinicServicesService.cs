using Application.Common;
using Application.DTOs.ClinicServices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ClinicServicesService : IClinicServicesService
{
    private readonly ICrudRepository<Domain.Entities.Services, int> _services;
    private readonly ICrudRepository<ServiceCategories, short> _categories;

    public ClinicServicesService(
        ICrudRepository<Domain.Entities.Services, int> services,
        ICrudRepository<ServiceCategories, short> categories)
    {
        _services = services;
        _categories = categories;
    }

    public async Task<IEnumerable<ClinicServiceDto>> GetAllAsync()
        => (await _services.GetAllAsync()).Select(Map);

    public async Task<ClinicServiceDto?> GetByIdAsync(int id)
    {
        var entity = await _services.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<ClinicServiceDto> CreateAsync(ClinicServiceDto dto)
    {
        Guard.Found(await _categories.GetByIdAsync(dto.CategoryId), $"Category {dto.CategoryId}");
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Service name is required");
        var entity = new Domain.Entities.Services
        {
            CategoryId = dto.CategoryId,
            Name = dto.Name.Trim(),
            Description = dto.Description,
            DurationMin = dto.DurationMin,
            IsActive = dto.IsActive
        };
        await _services.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(ClinicServiceDto dto)
    {
        var existing = Guard.Found(await _services.GetByIdAsync(dto.ServiceId), "Service");
        Guard.Found(await _categories.GetByIdAsync(dto.CategoryId), $"Category {dto.CategoryId}");
        existing.CategoryId = dto.CategoryId;
        existing.Name = dto.Name;
        existing.Description = dto.Description;
        existing.DurationMin = dto.DurationMin;
        existing.IsActive = dto.IsActive;
        await _services.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Guard.Found(await _services.GetByIdAsync(id), "Service");
        await _services.DeleteAsync(id);
    }

    private static ClinicServiceDto Map(Domain.Entities.Services s) => new()
    {
        ServiceId = s.ServiceId,
        CategoryId = s.CategoryId,
        Name = s.Name,
        Description = s.Description,
        DurationMin = s.DurationMin,
        IsActive = s.IsActive
    };
}
