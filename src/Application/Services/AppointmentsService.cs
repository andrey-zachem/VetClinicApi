using Application.Common;
using Application.DTOs.Appointments;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AppointmentsService : IAppointmentsService
{
    private readonly IAppointmentsRepository _appointments;
    private readonly IPetsRepository _pets;
    private readonly IEmployeesRepository _employees;
    private readonly ICrudRepository<Rooms, short> _rooms;
    private readonly ICrudRepository<AppointmentStatuses, short> _statuses;
    private readonly IUserRepository _users;

    public AppointmentsService(
        IAppointmentsRepository appointments,
        IPetsRepository pets,
        IEmployeesRepository employees,
        ICrudRepository<Rooms, short> rooms,
        ICrudRepository<AppointmentStatuses, short> statuses,
        IUserRepository users)
    {
        _appointments = appointments;
        _pets = pets;
        _employees = employees;
        _rooms = rooms;
        _statuses = statuses;
        _users = users;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
        => (await _appointments.GetAllAsync()).Select(Map);

    public async Task<AppointmentDto?> GetByIdAsync(long id)
    {
        var entity = await _appointments.GetByIdAsync(id);
        return entity is null ? null : Map(entity);
    }

    public async Task<IEnumerable<AppointmentDto>> GetByPetIdAsync(long petId)
        => (await _appointments.GetByPetIdAsync(petId)).Select(Map);

    public async Task<IEnumerable<AppointmentDto>> GetByEmployeeIdAsync(long employeeId)
        => (await _appointments.GetByEmployeeIdAsync(employeeId)).Select(Map);

    public async Task<AppointmentDto> CreateAsync(AppointmentCreateDto dto)
    {
        await ValidateAsync(dto.PetId, dto.EmployeeId, dto.RoomId, dto.StatusId, dto.CreatedBy, dto.StartsAt, dto.EndsAt);
        var entity = new Appointments
        {
            PetId = dto.PetId,
            EmployeeId = dto.EmployeeId,
            RoomId = dto.RoomId,
            StatusId = dto.StatusId,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            Reason = dto.Reason,
            CreatedBy = dto.CreatedBy
        };
        await _appointments.AddAsync(entity);
        return Map(entity);
    }

    public async Task UpdateAsync(AppointmentDto dto)
    {
        var existing = Guard.Found(await _appointments.GetByIdAsync(dto.AppointmentId), "Appointment");
        await ValidateAsync(dto.PetId, dto.EmployeeId, dto.RoomId, dto.StatusId, dto.CreatedBy, dto.StartsAt, dto.EndsAt);
        existing.PetId = dto.PetId;
        existing.EmployeeId = dto.EmployeeId;
        existing.RoomId = dto.RoomId;
        existing.StatusId = dto.StatusId;
        existing.StartsAt = dto.StartsAt;
        existing.EndsAt = dto.EndsAt;
        existing.Reason = dto.Reason;
        existing.CreatedBy = dto.CreatedBy;
        await _appointments.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _appointments.GetByIdAsync(id), "Appointment");
        await _appointments.DeleteAsync(id);
    }

    private async Task ValidateAsync(
        long petId, long employeeId, short? roomId, short statusId, long? createdBy,
        DateTimeOffset startsAt, DateTimeOffset endsAt)
    {
        if (endsAt <= startsAt)
            throw new ArgumentException("EndsAt must be later than StartsAt");
        Guard.Found(await _pets.GetByIdAsync(petId), $"Pet {petId}");
        Guard.Found(await _employees.GetByIdAsync(employeeId), $"Employee {employeeId}");
        Guard.Found(await _statuses.GetByIdAsync(statusId), $"Appointment status {statusId}");
        if (roomId is not null)
            Guard.Found(await _rooms.GetByIdAsync(roomId.Value), $"Room {roomId}");
        if (createdBy is not null)
            Guard.Found(await _users.GetByIdAsync(createdBy.Value), $"User {createdBy}");
    }

    private static AppointmentDto Map(Appointments a) => new()
    {
        AppointmentId = a.AppointmentId,
        PetId = a.PetId,
        EmployeeId = a.EmployeeId,
        RoomId = a.RoomId,
        StatusId = a.StatusId,
        StatusName = a.Status?.Name,
        RoomName = a.Room?.Name,
        StartsAt = a.StartsAt,
        EndsAt = a.EndsAt,
        Reason = a.Reason,
        CreatedBy = a.CreatedBy,
        CreatedAt = a.CreatedAt
    };
}
