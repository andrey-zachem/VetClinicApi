using Application.DTOs.Client;

namespace Application.Interfaces;

public interface IClientsService
{
    Task<ClientDto?> GetByIdAsync(long id);
    Task<IEnumerable<ClientDto>> GetAllAsync();
    Task<ClientDto> CreateAsync(ClientCreateDto dto);
    Task UpdateAsync(ClientDto dto);
    Task DeleteAsync(long id);
}
