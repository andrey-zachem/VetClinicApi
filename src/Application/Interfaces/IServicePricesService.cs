using Application.DTOs.ClinicServices;

namespace Application.Interfaces;

public interface IServicePricesService
{
    Task<IEnumerable<ServicePriceDto>> GetAllAsync();
    Task<ServicePriceDto?> GetByIdAsync(long id);
    Task<IEnumerable<ServicePriceDto>> GetByServiceIdAsync(int serviceId);
    Task<ServicePriceDto> CreateAsync(ServicePriceDto dto);
    Task UpdateAsync(ServicePriceDto dto);
    Task DeleteAsync(long id);
}
