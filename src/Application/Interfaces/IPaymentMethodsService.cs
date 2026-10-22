using Application.DTOs.Invoices;

namespace Application.Interfaces;

public interface IPaymentMethodsService
{
    Task<IEnumerable<PaymentMethodDto>> GetAllAsync();
    Task<PaymentMethodDto?> GetByIdAsync(short id);
    Task<PaymentMethodDto> CreateAsync(PaymentMethodDto dto);
    Task UpdateAsync(PaymentMethodDto dto);
    Task DeleteAsync(short id);
}
