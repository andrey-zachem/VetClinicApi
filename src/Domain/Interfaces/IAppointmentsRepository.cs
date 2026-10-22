using Domain.Entities;

namespace Domain.Interfaces;

public interface IAppointmentsRepository : ICrudRepository<Appointments, long>
{
    Task<IEnumerable<Appointments>> GetByPetIdAsync(long petId);
    Task<IEnumerable<Appointments>> GetByEmployeeIdAsync(long employeeId);
}
