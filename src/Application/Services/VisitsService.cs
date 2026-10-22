using Application.Common;
using Application.DTOs.Visits;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class VisitsService : IVisitsService
{
    private readonly IVisitsRepository _visits;
    private readonly IPetsRepository _pets;
    private readonly IEmployeesRepository _employees;
    private readonly IAppointmentsRepository _appointments;
    private readonly ICrudRepository<Diagnoses, int> _diagnoses;

    public VisitsService(
        IVisitsRepository visits,
        IPetsRepository pets,
        IEmployeesRepository employees,
        IAppointmentsRepository appointments,
        ICrudRepository<Diagnoses, int> diagnoses)
    {
        _visits = visits;
        _pets = pets;
        _employees = employees;
        _appointments = appointments;
        _diagnoses = diagnoses;
    }

    public async Task<IEnumerable<VisitDto>> GetAllAsync()
        => (await _visits.GetAllAsync()).Select(Map);

    public async Task<VisitDto?> GetByIdAsync(long id)
    {
        var entity = await _visits.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<VisitDto>> GetByPetIdAsync(long petId)
        => (await _visits.GetByPetIdAsync(petId)).Select(Map);

    public async Task<VisitDto> CreateAsync(VisitCreateDto dto)
    {
        await ValidateAsync(dto.PetId, dto.EmployeeId, dto.AppointmentId, dto.Diagnoses);
        var entity = new Visits
        {
            AppointmentId = dto.AppointmentId,
            PetId = dto.PetId,
            EmployeeId = dto.EmployeeId,
            VisitedAt = dto.VisitedAt ?? DateTimeOffset.UtcNow,
            WeightKg = dto.WeightKg,
            TemperatureC = dto.TemperatureC,
            Complaints = dto.Complaints,
            Anamnesis = dto.Anamnesis,
            Examination = dto.Examination,
            TreatmentPlan = dto.TreatmentPlan,
            Recommendations = dto.Recommendations
        };
        await _visits.AddAsync(entity);
        if (dto.Diagnoses.Count > 0)
            await _visits.ReplaceDiagnosesAsync(entity.VisitId, dto.Diagnoses.Select(d => (d.DiagnosisId, d.IsPrimary)));
        return Map(Guard.Found(await _visits.GetByIdAsync(entity.VisitId), "Visit"));
    }

    public async Task UpdateAsync(VisitDto dto)
    {
        var existing = Guard.Found(await _visits.GetByIdAsync(dto.VisitId), "Visit");
        await ValidateAsync(dto.PetId, dto.EmployeeId, dto.AppointmentId, dto.Diagnoses);
        existing.AppointmentId = dto.AppointmentId;
        existing.PetId = dto.PetId;
        existing.EmployeeId = dto.EmployeeId;
        existing.VisitedAt = dto.VisitedAt;
        existing.WeightKg = dto.WeightKg;
        existing.TemperatureC = dto.TemperatureC;
        existing.Complaints = dto.Complaints;
        existing.Anamnesis = dto.Anamnesis;
        existing.Examination = dto.Examination;
        existing.TreatmentPlan = dto.TreatmentPlan;
        existing.Recommendations = dto.Recommendations;
        await _visits.UpdateAsync(existing);
        await _visits.ReplaceDiagnosesAsync(dto.VisitId, dto.Diagnoses.Select(d => (d.DiagnosisId, d.IsPrimary)));
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _visits.GetByIdAsync(id), "Visit");
        await _visits.DeleteAsync(id);
    }

    private async Task ValidateAsync(
        long petId, long employeeId, long? appointmentId, IEnumerable<VisitDiagnosisDto> diagnoses)
    {
        Guard.Found(await _pets.GetByIdAsync(petId), $"Pet {petId}");
        Guard.Found(await _employees.GetByIdAsync(employeeId), $"Employee {employeeId}");
        if (appointmentId is not null)
            Guard.Found(await _appointments.GetByIdAsync(appointmentId.Value), $"Appointment {appointmentId}");
        foreach (var diagnosis in diagnoses)
            Guard.Found(await _diagnoses.GetByIdAsync(diagnosis.DiagnosisId), $"Diagnosis {diagnosis.DiagnosisId}");
    }

    private static VisitDto Map(Visits v) => new()
    {
        VisitId = v.VisitId,
        AppointmentId = v.AppointmentId,
        PetId = v.PetId,
        EmployeeId = v.EmployeeId,
        VisitedAt = v.VisitedAt,
        WeightKg = v.WeightKg,
        TemperatureC = v.TemperatureC,
        Complaints = v.Complaints,
        Anamnesis = v.Anamnesis,
        Examination = v.Examination,
        TreatmentPlan = v.TreatmentPlan,
        Recommendations = v.Recommendations,
        Diagnoses = v.Diagnoses.Select(d => new VisitDiagnosisDto
        {
            DiagnosisId = d.DiagnosisId,
            DiagnosisName = d.Diagnosis?.Name,
            IsPrimary = d.IsPrimary
        }).ToList()
    };
}
