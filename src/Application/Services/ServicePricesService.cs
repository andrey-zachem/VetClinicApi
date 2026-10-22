using Application.Common;
using Application.DTOs.ClinicServices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ServicePricesService : IServicePricesService
{
    private readonly IServicePricesRepository _prices;
    private readonly ICrudRepository<Domain.Entities.Services, int> _services;

    public ServicePricesService(IServicePricesRepository prices, ICrudRepository<Domain.Entities.Services, int> services)
    {
        _prices = prices;
        _services = services;
    }

    public async Task<IEnumerable<ServicePriceDto>> GetAllAsync()
        => (await _prices.GetAllAsync()).Select(Map);

    public async Task<ServicePriceDto?> GetByIdAsync(long id)
    {
        var entity = await _prices.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<ServicePriceDto>> GetByServiceIdAsync(int serviceId)
        => (await _prices.GetByServiceIdAsync(serviceId)).Select(Map);

    public async Task<ServicePriceDto> CreateAsync(ServicePriceDto dto)
    {
        Guard.Found(await _services.GetByIdAsync(dto.ServiceId), $"Service {dto.ServiceId}");
        if (dto.Price < 0)
            throw new ArgumentException("Price cannot be negative");
        var entity = new ServicePrices
        {
            ServiceId = dto.ServiceId,
            Price = dto.Price,
            ValidFrom = dto.ValidFrom
        };
        await _prices.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(ServicePriceDto dto)
    {
        var existing = Guard.Found(await _prices.GetByIdAsync(dto.PriceId), "Service price");
        Guard.Found(await _services.GetByIdAsync(dto.ServiceId), $"Service {dto.ServiceId}");
        existing.ServiceId = dto.ServiceId;
        existing.Price = dto.Price;
        existing.ValidFrom = dto.ValidFrom;
        await _prices.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _prices.GetByIdAsync(id), "Service price");
        await _prices.DeleteAsync(id);
    }

    private static ServicePriceDto Map(ServicePrices p) => new()
    {
        PriceId = p.PriceId,
        ServiceId = p.ServiceId,
        Price = p.Price,
        ValidFrom = p.ValidFrom
    };
}
