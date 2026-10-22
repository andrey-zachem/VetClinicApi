using Application.Common;
using Application.DTOs.Vaccines;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class VaccinationsService : IVaccinationsService
{
    private readonly IVaccinationsRepository _vaccinations;
    private readonly IPetsRepository _pets;
    private readonly IVaccineBatchesRepository _batches;
    private readonly IEmployeesRepository _employees;
    private readonly IVisitsRepository _visits;

    public VaccinationsService(
        IVaccinationsRepository vaccinations,
        IPetsRepository pets,
        IVaccineBatchesRepository batches,
        IEmployeesRepository employees,
        IVisitsRepository visits)
    {
        _vaccinations = vaccinations;
        _pets = pets;
        _batches = batches;
        _employees = employees;
        _visits = visits;
    }

    public async Task<IEnumerable<VaccinationDto>> GetAllAsync()
        => (await _vaccinations.GetAllAsync()).Select(Map);

    public async Task<VaccinationDto?> GetByIdAsync(long id)
    {
        var entity = await _vaccinations.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<VaccinationDto>> GetByPetIdAsync(long petId)
        => (await _vaccinations.GetByPetIdAsync(petId)).Select(Map);

    public async Task<VaccinationDto> CreateAsync(VaccinationDto dto)
    {
        await ValidateAsync(dto);
        var entity = new Vaccinations
        {
            PetId = dto.PetId,
            BatchId = dto.BatchId,
            EmployeeId = dto.EmployeeId,
            VisitId = dto.VisitId,
            VaccinatedOn = dto.VaccinatedOn == default ? DateOnly.FromDateTime(DateTime.UtcNow) : dto.VaccinatedOn,
            Note = dto.Note
        };
        await _vaccinations.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(VaccinationDto dto)
    {
        var existing = Guard.Found(await _vaccinations.GetByIdAsync(dto.VaccinationId), "Vaccination");
        await ValidateAsync(dto);
        existing.PetId = dto.PetId;
        existing.BatchId = dto.BatchId;
        existing.EmployeeId = dto.EmployeeId;
        existing.VisitId = dto.VisitId;
        existing.VaccinatedOn = dto.VaccinatedOn;
        existing.Note = dto.Note;
        await _vaccinations.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _vaccinations.GetByIdAsync(id), "Vaccination");
        await _vaccinations.DeleteAsync(id);
    }

    private async Task ValidateAsync(VaccinationDto dto)
    {
        Guard.Found(await _pets.GetByIdAsync(dto.PetId), $"Pet {dto.PetId}");
        var batch = Guard.Found(await _batches.GetByIdAsync(dto.BatchId), $"Batch {dto.BatchId}");
        Guard.Found(await _employees.GetByIdAsync(dto.EmployeeId), $"Employee {dto.EmployeeId}");
        if (dto.VisitId is not null)
            Guard.Found(await _visits.GetByIdAsync(dto.VisitId.Value), $"Visit {dto.VisitId}");
        var vaccinatedOn = dto.VaccinatedOn == default ? DateOnly.FromDateTime(DateTime.UtcNow) : dto.VaccinatedOn;
        if (batch.ExpiresOn < vaccinatedOn)
            throw new ArgumentException("Vaccine batch is expired");
    }

    private static VaccinationDto Map(Vaccinations v) => new()
    {
        VaccinationId = v.VaccinationId,
        PetId = v.PetId,
        BatchId = v.BatchId,
        EmployeeId = v.EmployeeId,
        VisitId = v.VisitId,
        VaccinatedOn = v.VaccinatedOn,
        Note = v.Note
    };
}
