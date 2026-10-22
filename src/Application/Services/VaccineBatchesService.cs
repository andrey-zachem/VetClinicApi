using Application.Common;
using Application.DTOs.Vaccines;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class VaccineBatchesService : IVaccineBatchesService
{
    private readonly IVaccineBatchesRepository _batches;
    private readonly ICrudRepository<Vaccines, int> _vaccines;

    public VaccineBatchesService(IVaccineBatchesRepository batches, ICrudRepository<Vaccines, int> vaccines)
    {
        _batches = batches;
        _vaccines = vaccines;
    }

    public async Task<IEnumerable<VaccineBatchDto>> GetAllAsync()
        => (await _batches.GetAllAsync()).Select(Map);

    public async Task<VaccineBatchDto?> GetByIdAsync(long id)
    {
        var entity = await _batches.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<VaccineBatchDto>> GetByVaccineIdAsync(int vaccineId)
        => (await _batches.GetByVaccineIdAsync(vaccineId)).Select(Map);

    public async Task<VaccineBatchDto> CreateAsync(VaccineBatchDto dto)
    {
        Guard.Found(await _vaccines.GetByIdAsync(dto.VaccineId), $"Vaccine {dto.VaccineId}");
        if (string.IsNullOrWhiteSpace(dto.BatchNo))
            throw new ArgumentException("Batch number is required");
        var entity = new VaccineBatches
        {
            VaccineId = dto.VaccineId,
            BatchNo = dto.BatchNo.Trim(),
            ExpiresOn = dto.ExpiresOn
        };
        await _batches.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(VaccineBatchDto dto)
    {
        var existing = Guard.Found(await _batches.GetByIdAsync(dto.BatchId), "Vaccine batch");
        Guard.Found(await _vaccines.GetByIdAsync(dto.VaccineId), $"Vaccine {dto.VaccineId}");
        existing.VaccineId = dto.VaccineId;
        existing.BatchNo = dto.BatchNo;
        existing.ExpiresOn = dto.ExpiresOn;
        await _batches.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _batches.GetByIdAsync(id), "Vaccine batch");
        await _batches.DeleteAsync(id);
    }

    private static VaccineBatchDto Map(VaccineBatches b) => new()
    {
        BatchId = b.BatchId,
        VaccineId = b.VaccineId,
        BatchNo = b.BatchNo,
        ExpiresOn = b.ExpiresOn
    };
}
