using Application.DTOs.Invoices;

namespace Application.Interfaces;

public interface IPaymentsService
{
    Task<IEnumerable<PaymentDto>> GetAllAsync();
    Task<PaymentDto?> GetByIdAsync(long id);
    Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(long invoiceId);
    Task<PaymentDto> CreateAsync(PaymentCreateDto dto);
    Task DeleteAsync(long id);
}
