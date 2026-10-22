using Application.Common;
using Application.DTOs.Breeds;
using Application.DTOs.Species;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class BreedsService : IBreedsService
{
    private readonly IBreedsRepository _breeds;
    private readonly ISpeciesRepository _species;

    public BreedsService(IBreedsRepository breeds, ISpeciesRepository species)
    {
        _breeds = breeds;
        _species = species;
    }

    public async Task<IEnumerable<BreedsDao>> GetAllAsync()
        => (await _breeds.GetAllAsync()).Select(Map);

    public async Task<BreedsDao?> GetByIdAsync(int id)
    {
        var entity = await _breeds.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<BreedsDao> CreateAsync(BreedCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Breed name is required");
        Guard.Found(await _species.GetByIdAsync(dto.SpeciesId), $"Species {dto.SpeciesId}");
        var entity = new Breeds { SpeciesId = dto.SpeciesId, Name = dto.Name.Trim() };
        await _breeds.AddAsync(entity);
        var created = Guard.Found(await _breeds.GetByIdAsync(entity.BreedsId), "Breed");
        return Map(created);
    }

    public async Task UpdateAsync(BreedsDao dto)
    {
        var existing = Guard.Found(await _breeds.GetByIdAsync(dto.BreedId), "Breed");
        Guard.Found(await _species.GetByIdAsync(dto.SpeciesId), $"Species {dto.SpeciesId}");
        existing.SpeciesId = dto.SpeciesId;
        existing.Name = dto.Name;
        await _breeds.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Guard.Found(await _breeds.GetByIdAsync(id), "Breed");
        await _breeds.DeleteAsync(id);
    }

    private static BreedsDao Map(Breeds b) => new()
    {
        BreedId = b.BreedsId,
        SpeciesId = b.SpeciesId,
        Name = b.Name,
        Species = b.Species is null ? null : new SpeciesDao { SpeciesId = b.Species.SpeciesId, Name = b.Species.Name }
    };
}
