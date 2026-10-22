using Application.DTOs.Employees.Positions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PositionsService : IPositionsService
{
    private readonly IPositionsRepository _positionsRepository;

    public PositionsService(IPositionsRepository positionsRepository)
    {
        _positionsRepository = positionsRepository;
    }

    public async Task<IEnumerable<PositionsDto>> GetAllPositionsAsync()
    {
        var positions = await _positionsRepository.GetAllAsync();
        return positions.Select(MapToDto);
    }

    public async Task<PositionsDto?> GetPositionByIdAsync(short id)
    {
        var position = await _positionsRepository.GetByIdAsync(id);
        return position is null ? null : MapToDto(position);
    }

    public async Task<PositionsDto> CreatePositionAsync(PositionsDto position)
    {
        var entity = new Positions { Name = position.Name };
        await _positionsRepository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task UpdatePositionAsync(PositionsDto position)
    {
        var existing = await _positionsRepository.GetByIdAsync(position.PositionId);
        if (existing == null)
            throw new KeyNotFoundException("Position not found");

        existing.Name = position.Name;
        await _positionsRepository.UpdateAsync(existing);
    }

    public async Task DeletePositionAsync(short id)
    {
        var existing = await _positionsRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Position not found");

        await _positionsRepository.DeleteAsync(id);
    }

    private static PositionsDto MapToDto(Positions position) => new()
    {
        PositionId = position.PositionId,
        Name = position.Name
    };
}
