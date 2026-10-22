using Application.DTOs.Species;

namespace Application.Interfaces;

public interface ISpeciesService
{
    Task<IEnumerable<SpeciesDao>> GetAllAsync();
    Task<SpeciesDao?> GetByIdAsync(short id);
    Task<SpeciesDao> CreateAsync(SpeciesDao dto);
    Task UpdateAsync(SpeciesDao dto);
    Task DeleteAsync(short id);
}
