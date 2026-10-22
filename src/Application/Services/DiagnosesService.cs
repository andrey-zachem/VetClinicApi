using Application.Common;
using Application.DTOs.Diagnoses;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class DiagnosesService : IDiagnosesService
{
    private readonly ICrudRepository<Diagnoses, int> _diagnoses;

    public DiagnosesService(ICrudRepository<Diagnoses, int> diagnoses)
    {
        _diagnoses = diagnoses;
    }

    public async Task<IEnumerable<DiagnosisDto>> GetAllAsync()
        => (await _diagnoses.GetAllAsync()).Select(Map);

    public async Task<DiagnosisDto?> GetByIdAsync(int id)
    {
        var entity = await _diagnoses.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<DiagnosisDto> CreateAsync(DiagnosisDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Diagnosis name is required");
        var entity = new Diagnoses { Name = dto.Name.Trim() };
        await _diagnoses.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(DiagnosisDto dto)
    {
        var existing = Guard.Found(await _diagnoses.GetByIdAsync(dto.DiagnosisId), "Diagnosis");
        existing.Name = dto.Name;
        await _diagnoses.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Guard.Found(await _diagnoses.GetByIdAsync(id), "Diagnosis");
        await _diagnoses.DeleteAsync(id);
    }

    private static DiagnosisDto Map(Diagnoses d) => new() { DiagnosisId = d.DiagnosisId, Name = d.Name };
}
