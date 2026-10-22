using Application.DTOs.Employees.Positions;

namespace Application.Interfaces;

public interface IPositionsService
{
    Task<IEnumerable<PositionsDto>> GetAllPositionsAsync();
    Task<PositionsDto?> GetPositionByIdAsync(short id);
    Task<PositionsDto> CreatePositionAsync(PositionsDto position);
    Task UpdatePositionAsync(PositionsDto position);
    Task DeletePositionAsync(short id);
}
