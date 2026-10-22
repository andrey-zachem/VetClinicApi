using Application.Common;
using Application.DTOs.Client;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ClientsService : IClientsService
{
    private readonly IClientRepository _clients;
    private readonly IUserRepository _users;

    public ClientsService(IClientRepository clients, IUserRepository users)
    {
        _clients = clients;
        _users = users;
    }

    public async Task<ClientDto?> GetByIdAsync(long id)
    {
        var client = await _clients.GetByIdAsync(id);
        return client is null ? null : Map(client);
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _clients.GetAllAsync();
        return clients.Select(Map);
    }

    public async Task<ClientDto> CreateAsync(ClientCreateDto dto)
    {
        Validate(dto.LastName, dto.FirstName, dto.PhoneNumber);
        await EnsureUserExists(dto.UserId);

        var entity = new Clients
        {
            LastName = dto.LastName.Trim(),
            FirstName = dto.FirstName.Trim(),
            MiddleName = dto.MiddleName,
            PhoneNumber = dto.PhoneNumber.Trim(),
            Email = dto.Email,
            Address = dto.Address,
            Note = dto.Note,
            UserId = dto.UserId,
            IsActive = true
        };
        await _clients.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(ClientDto dto)
    {
        Validate(dto.LastName, dto.FirstName, dto.PhoneNumber);
        var existing = Guard.Found(await _clients.GetByIdAsync(dto.ClientId), "Client");
        await EnsureUserExists(dto.UserId);

        existing.LastName = dto.LastName.Trim();
        existing.FirstName = dto.FirstName.Trim();
        existing.MiddleName = dto.MiddleName;
        existing.PhoneNumber = dto.PhoneNumber.Trim();
        existing.Email = dto.Email;
        existing.Address = dto.Address;
        existing.Note = dto.Note;
        existing.UserId = dto.UserId;
        existing.IsActive = dto.IsActive;
        await _clients.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _clients.GetByIdAsync(id), "Client");
        await _clients.DeleteAsync(id);
    }

    private async Task EnsureUserExists(long? userId)
    {
        if (userId is null)
            return;
        Guard.Found(await _users.GetByIdAsync(userId.Value), $"User {userId}");
    }

    private static void Validate(string lastName, string firstName, string phone)
    {
        if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name and last name are required");
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone is required");
    }

    private static ClientDto Map(Clients c) => new()
    {
        ClientId = c.ClientId,
        LastName = c.LastName,
        FirstName = c.FirstName,
        MiddleName = c.MiddleName,
        PhoneNumber = c.PhoneNumber,
        Email = c.Email,
        Address = c.Address,
        Note = c.Note,
        IsActive = c.IsActive,
        UserId = c.UserId,
        CreatedAt = c.CreatedAt
    };
}
