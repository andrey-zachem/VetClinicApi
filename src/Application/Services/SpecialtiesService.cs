using Application.DTOs.Employees.Specialties;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class SpecialtiesService : ISpecialtiesService
{
    private readonly ISpecialtiesRepository _specialtiesRepository;

    public SpecialtiesService(ISpecialtiesRepository specialtiesRepository)
    {
        _specialtiesRepository = specialtiesRepository;
    }

    public async Task<IEnumerable<SpecialtiesDto>> GetAllSpecialtiesAsync()
    {
        var specialties = await _specialtiesRepository.GetAllAsync();
        return specialties.Select(MapToDto);
    }

    public async Task<SpecialtiesDto?> GetSpecialtyByIdAsync(short id)
    {
        var specialty = await _specialtiesRepository.GetByIdAsync(id);
        return specialty is null ? null : MapToDto(specialty);
    }

    public async Task<SpecialtiesDto> CreateSpecialtyAsync(SpecialtiesDto specialty)
    {
        var entity = new Specialties { Name = specialty.Name };
        await _specialtiesRepository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task UpdateSpecialtyAsync(SpecialtiesDto specialty)
    {
        var existing = await _specialtiesRepository.GetByIdAsync(specialty.SpecialtyId);
        if (existing == null)
            throw new KeyNotFoundException("Specialty not found");

        existing.Name = specialty.Name;
        await _specialtiesRepository.UpdateAsync(existing);
    }

    public async Task DeleteSpecialtyAsync(short id)
    {
        var existing = await _specialtiesRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Specialty not found");

        await _specialtiesRepository.DeleteAsync(id);
    }

    private static SpecialtiesDto MapToDto(Specialties specialty) => new()
    {
        SpecialtyId = specialty.SpecialtyId,
        Name = specialty.Name
    };
}
