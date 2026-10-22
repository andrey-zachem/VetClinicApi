using Application.Common;
using Application.DTOs.Species;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepository _species;

    public SpeciesService(ISpeciesRepository species)
    {
        _species = species;
    }

    public async Task<IEnumerable<SpeciesDao>> GetAllAsync()
        => (await _species.GetAllAsync()).Select(Map);

    public async Task<SpeciesDao?> GetByIdAsync(short id)
    {
        var entity = await _species.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<SpeciesDao> CreateAsync(SpeciesDao dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Species name is required");
        var entity = new Species { Name = dto.Name.Trim() };
        await _species.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(SpeciesDao dto)
    {
        var existing = Guard.Found(await _species.GetByIdAsync(dto.SpeciesId), "Species");
        existing.Name = dto.Name;
        await _species.UpdateAsync(existing);
    }

    public async Task DeleteAsync(short id)
    {
        Guard.Found(await _species.GetByIdAsync(id), "Species");
        await _species.DeleteAsync(id);
    }

    private static SpeciesDao Map(Species s) => new() { SpeciesId = s.SpeciesId, Name = s.Name };
}
