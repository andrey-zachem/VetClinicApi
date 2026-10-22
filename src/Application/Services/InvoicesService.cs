using Application.Common;
using Application.DTOs.Invoices;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class InvoicesService : IInvoicesService
{
    private readonly IInvoicesRepository _invoices;
    private readonly IVisitsRepository _visits;
    private readonly IClientRepository _clients;
    private readonly ICrudRepository<Domain.Entities.Services, int> _services;
    private readonly IEmployeesRepository _employees;
    private readonly IUserRepository _users;

    public InvoicesService(
        IInvoicesRepository invoices,
        IVisitsRepository visits,
        IClientRepository clients,
        ICrudRepository<Domain.Entities.Services, int> services,
        IEmployeesRepository employees,
        IUserRepository users)
    {
        _invoices = invoices;
        _visits = visits;
        _clients = clients;
        _services = services;
        _employees = employees;
        _users = users;
    }

    public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        => (await _invoices.GetAllAsync()).Select(Map);

    public async Task<InvoiceDto?> GetByIdAsync(long id)
    {
        var entity = await _invoices.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<InvoiceDto>> GetByClientIdAsync(long clientId)
        => (await _invoices.GetByClientIdAsync(clientId)).Select(Map);

    public async Task<IEnumerable<InvoiceDto>> GetByVisitIdAsync(long visitId)
        => (await _invoices.GetByVisitIdAsync(visitId)).Select(Map);

    public async Task<InvoiceDto> CreateAsync(InvoiceCreateDto dto)
    {
        Guard.Found(await _visits.GetByIdAsync(dto.VisitId), $"Visit {dto.VisitId}");
        Guard.Found(await _clients.GetByIdAsync(dto.ClientId), $"Client {dto.ClientId}");
        if (dto.CreatedBy is not null)
            Guard.Found(await _users.GetByIdAsync(dto.CreatedBy.Value), $"User {dto.CreatedBy}");
        if (dto.Items.Count == 0)
            throw new ArgumentException("Invoice must contain at least one item");

        var entity = new Invoices
        {
            VisitId = dto.VisitId,
            ClientId = dto.ClientId,
            CreatedBy = dto.CreatedBy
        };

        foreach (var item in dto.Items)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            if (item.UnitPrice < 0 || item.Discount < 0)
                throw new ArgumentException("Price and discount cannot be negative");
            Guard.Found(await _services.GetByIdAsync(item.ServiceId), $"Service {item.ServiceId}");
            if (item.EmployeeId is not null)
                Guard.Found(await _employees.GetByIdAsync(item.EmployeeId.Value), $"Employee {item.EmployeeId}");

            entity.Items.Add(new InvoiceItems
            {
                ServiceId = item.ServiceId,
                EmployeeId = item.EmployeeId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount
            });
        }

        await _invoices.AddAsync(entity);
        return Map(Guard.Found(await _invoices.GetByIdAsync(entity.InvoiceId), "Invoice"));
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _invoices.GetByIdAsync(id), "Invoice");
        await _invoices.DeleteAsync(id);
    }

    private static InvoiceDto Map(Invoices invoice)
    {
        var items = invoice.Items.Select(i => new InvoiceItemDto
        {
            ItemId = i.ItemId,
            ServiceId = i.ServiceId,
            EmployeeId = i.EmployeeId,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            Discount = i.Discount,
            LineTotal = i.Quantity * i.UnitPrice - i.Discount
        }).ToList();
        var payments = invoice.Payments.Select(p => new PaymentDto
        {
            PaymentId = p.PaymentId,
            InvoiceId = p.InvoiceId,
            MethodId = p.MethodId,
            Amount = p.Amount,
            PaidAt = p.PaidAt
        }).ToList();

        return new InvoiceDto
        {
            InvoiceId = invoice.InvoiceId,
            VisitId = invoice.VisitId,
            ClientId = invoice.ClientId,
            IssuedAt = invoice.IssuedAt,
            CreatedBy = invoice.CreatedBy,
            Items = items,
            Payments = payments,
            Total = items.Sum(i => i.LineTotal),
            Paid = payments.Sum(p => p.Amount)
        };
    }
}
