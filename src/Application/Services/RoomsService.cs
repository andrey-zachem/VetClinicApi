using Application.Common;
using Application.DTOs.Rooms;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class RoomsService : IRoomsService
{
    private readonly ICrudRepository<Rooms, short> _rooms;

    public RoomsService(ICrudRepository<Rooms, short> rooms)
    {
        _rooms = rooms;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
        => (await _rooms.GetAllAsync()).Select(Map);

    public async Task<RoomDto?> GetByIdAsync(short id)
    {
        var entity = await _rooms.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<RoomDto> CreateAsync(RoomDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Room name is required");
        var entity = new Rooms { Name = dto.Name.Trim() };
        await _rooms.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(RoomDto dto)
    {
        var existing = Guard.Found(await _rooms.GetByIdAsync(dto.RoomId), "Room");
        existing.Name = dto.Name;
        await _rooms.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _rooms.GetByIdAsync(id), "Room");
        await _rooms.DeleteAsync(id);
    }

    private static RoomDto Map(Rooms r) => new() { RoomId = r.RoomId, Name = r.Name };
}
