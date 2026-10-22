using Application.DTOs.Invoices;

namespace Application.Interfaces;

public interface IInvoicesService
{
    Task<IEnumerable<InvoiceDto>> GetAllAsync();
    Task<InvoiceDto?> GetByIdAsync(long id);
    Task<IEnumerable<InvoiceDto>> GetByClientIdAsync(long clientId);
    Task<IEnumerable<InvoiceDto>> GetByVisitIdAsync(long visitId);
    Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto);
    Task DeleteAsync(long id);
}
