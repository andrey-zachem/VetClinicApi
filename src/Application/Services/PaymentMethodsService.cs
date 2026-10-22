using Application.Common;
using Application.DTOs.Invoices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PaymentMethodsService : IPaymentMethodsService
{
    private readonly ICrudRepository<PaymentMethods, short> _methods;

    public PaymentMethodsService(ICrudRepository<PaymentMethods, short> methods)
    {
        _methods = methods;
    }

    public async Task<IEnumerable<PaymentMethodDto>> GetAllAsync()
        => (await _methods.GetAllAsync()).Select(Map);

    public async Task<PaymentMethodDto?> GetByIdAsync(short id)
    {
        var entity = await _methods.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<PaymentMethodDto> CreateAsync(PaymentMethodDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Payment method name is required");

        var entity = new PaymentMethods { Name = dto.Name.Trim() };
        await _methods.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(PaymentMethodDto dto)
    {
        var existing = Guard.Found(await _methods.GetByIdAsync(dto.MethodId), "Payment method");
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Payment method name is required");

        existing.Name = dto.Name.Trim();
        await _methods.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _methods.GetByIdAsync(id), "Payment method");
        await _methods.DeleteAsync(id);
    }

    private static PaymentMethodDto Map(PaymentMethods method) => new()
    {
        MethodId = method.MethodId,
        Name = method.Name
    };
}
