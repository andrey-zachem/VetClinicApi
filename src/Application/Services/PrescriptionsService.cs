using Application.Common;
using Application.DTOs.Prescriptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly IPrescriptionsRepository _prescriptions;
    private readonly IVisitsRepository _visits;
    private readonly ICrudRepository<Medications, int> _medications;

    public PrescriptionsService(
        IPrescriptionsRepository prescriptions,
        IVisitsRepository visits,
        ICrudRepository<Medications, int> medications)
    {
        _prescriptions = prescriptions;
        _visits = visits;
        _medications = medications;
    }

    public async Task<IEnumerable<PrescriptionDto>> GetAllAsync()
        => (await _prescriptions.GetAllAsync()).Select(Map);

    public async Task<PrescriptionDto?> GetByIdAsync(long id)
    {
        var entity = await _prescriptions.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<PrescriptionDto>> GetByVisitIdAsync(long visitId)
        => (await _prescriptions.GetByVisitIdAsync(visitId)).Select(Map);

    public async Task<PrescriptionDto> CreateAsync(PrescriptionDto dto)
    {
        await ValidateAsync(dto);
        var entity = new Prescriptions
        {
            VisitId = dto.VisitId,
            MedicationId = dto.MedicationId,
            Dosage = dto.Dosage.Trim(),
            Frequency = dto.Frequency,
            DurationDays = dto.DurationDays,
            Instructions = dto.Instructions
        };
        await _prescriptions.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(PrescriptionDto dto)
    {
        var existing = Guard.Found(await _prescriptions.GetByIdAsync(dto.PrescriptionId), "Prescription");
        await ValidateAsync(dto);
        existing.VisitId = dto.VisitId;
        existing.MedicationId = dto.MedicationId;
        existing.Dosage = dto.Dosage;
        existing.Frequency = dto.Frequency;
        existing.DurationDays = dto.DurationDays;
        existing.Instructions = dto.Instructions;
        await _prescriptions.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _prescriptions.GetByIdAsync(id), "Prescription");
        await _prescriptions.DeleteAsync(id);
    }

    private async Task ValidateAsync(PrescriptionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Dosage))
            throw new ArgumentException("Dosage is required");
        Guard.Found(await _visits.GetByIdAsync(dto.VisitId), $"Visit {dto.VisitId}");
        Guard.Found(await _medications.GetByIdAsync(dto.MedicationId), $"Medication {dto.MedicationId}");
    }

    private static PrescriptionDto Map(Prescriptions p) => new()
    {
        PrescriptionId = p.PrescriptionId,
        VisitId = p.VisitId,
        MedicationId = p.MedicationId,
        Dosage = p.Dosage,
        Frequency = p.Frequency,
        DurationDays = p.DurationDays,
        Instructions = p.Instructions
    };
}
