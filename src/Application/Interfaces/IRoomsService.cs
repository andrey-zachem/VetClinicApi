using Application.DTOs.Rooms;

namespace Application.Interfaces;

public interface IRoomsService
{
    Task<IEnumerable<RoomDto>> GetAllAsync();
    Task<RoomDto?> GetByIdAsync(short id);
    Task<RoomDto> CreateAsync(RoomDto dto);
    Task UpdateAsync(RoomDto dto);
    Task DeleteAsync(short id);
}
