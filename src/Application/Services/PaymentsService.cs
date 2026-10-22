using Application.Common;
using Application.DTOs.Invoices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IPaymentsRepository _payments;
    private readonly IInvoicesRepository _invoices;
    private readonly ICrudRepository<PaymentMethods, short> _methods;

    public PaymentsService(
        IPaymentsRepository payments,
        IInvoicesRepository invoices,
        ICrudRepository<PaymentMethods, short> methods)
    {
        _payments = payments;
        _invoices = invoices;
        _methods = methods;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        => (await _payments.GetAllAsync()).Select(Map);

    public async Task<PaymentDto?> GetByIdAsync(long id)
    {
        var entity = await _payments.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<PaymentDto>> GetByInvoiceIdAsync(long invoiceId)
        => (await _payments.GetByInvoiceIdAsync(invoiceId)).Select(Map);

    public async Task<PaymentDto> CreateAsync(PaymentCreateDto dto)
    {
        if (dto.Amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Guard.Found(await _invoices.GetByIdAsync(dto.InvoiceId), $"Invoice {dto.InvoiceId}");
        Guard.Found(await _methods.GetByIdAsync(dto.MethodId), $"Payment method {dto.MethodId}");

        var entity = new Payments
        {
            InvoiceId = dto.InvoiceId,
            MethodId = dto.MethodId,
            Amount = dto.Amount,
            PaidAt = DateTimeOffset.UtcNow
        };
        await _payments.AddAsync(entity);
        return Map(entity);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _payments.GetByIdAsync(id), "Payment");
        await _payments.DeleteAsync(id);
    }

    private static PaymentDto Map(Payments payment) => new()
    {
        PaymentId = payment.PaymentId,
        InvoiceId = payment.InvoiceId,
        MethodId = payment.MethodId,
        Amount = payment.Amount,
        PaidAt = payment.PaidAt
    };
}
