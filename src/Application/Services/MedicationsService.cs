using Application.Common;
using Application.DTOs.Medications;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class MedicationsService : IMedicationsService
{
    private readonly ICrudRepository<Medications, int> _medications;

    public MedicationsService(ICrudRepository<Medications, int> medications)
    {
        _medications = medications;
    }

    public async Task<IEnumerable<MedicationDto>> GetAllAsync()
        => (await _medications.GetAllAsync()).Select(Map);

    public async Task<MedicationDto?> GetByIdAsync(int id)
    {
        var entity = await _medications.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<MedicationDto> CreateAsync(MedicationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Medication name is required");
        var entity = new Medications
        {
            Name = dto.Name.Trim(),
            Form = dto.Form,
            ActiveSubstance = dto.ActiveSubstance
        };
        await _medications.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(MedicationDto dto)
    {
        var existing = Guard.Found(await _medications.GetByIdAsync(dto.MedicationId), "Medication");
        existing.Name = dto.Name;
        existing.Form = dto.Form;
        existing.ActiveSubstance = dto.ActiveSubstance;
        await _medications.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Guard.Found(await _medications.GetByIdAsync(id), "Medication");
        await _medications.DeleteAsync(id);
    }

    private static MedicationDto Map(Medications m) => new()
    {
        MedicationId = m.MedicationId,
        Name = m.Name,
        Form = m.Form,
        ActiveSubstance = m.ActiveSubstance
    };
}
