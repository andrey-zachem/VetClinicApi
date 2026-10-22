using Application.Common;
using Application.DTOs.Vaccines;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class VaccinesService : IVaccinesService
{
    private readonly ICrudRepository<Vaccines, int> _vaccines;

    public VaccinesService(ICrudRepository<Vaccines, int> vaccines)
    {
        _vaccines = vaccines;
    }

    public async Task<IEnumerable<VaccineDto>> GetAllAsync()
        => (await _vaccines.GetAllAsync()).Select(Map);

    public async Task<VaccineDto?> GetByIdAsync(int id)
    {
        var entity = await _vaccines.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<VaccineDto> CreateAsync(VaccineDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.ProtectsAgainst))
            throw new ArgumentException("Name and protects_against are required");
        if (dto.ValidityDays <= 0)
            throw new ArgumentException("ValidityDays must be positive");
        var entity = new Vaccines
        {
            Name = dto.Name.Trim(),
            Manufacturer = dto.Manufacturer,
            ProtectsAgainst = dto.ProtectsAgainst.Trim(),
            ValidityDays = dto.ValidityDays
        };
        await _vaccines.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(VaccineDto dto)
    {
        var existing = Guard.Found(await _vaccines.GetByIdAsync(dto.VaccineId), "Vaccine");
        existing.Name = dto.Name;
        existing.Manufacturer = dto.Manufacturer;
        existing.ProtectsAgainst = dto.ProtectsAgainst;
        existing.ValidityDays = dto.ValidityDays;
        await _vaccines.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Guard.Found(await _vaccines.GetByIdAsync(id), "Vaccine");
        await _vaccines.DeleteAsync(id);
    }

    private static VaccineDto Map(Vaccines v) => new()
    {
        VaccineId = v.VaccineId,
        Name = v.Name,
        Manufacturer = v.Manufacturer,
        ProtectsAgainst = v.ProtectsAgainst,
        ValidityDays = v.ValidityDays
    };
}
