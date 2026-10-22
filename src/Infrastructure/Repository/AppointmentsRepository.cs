using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AppointmentsRepository : CrudRepository<Appointments, long>, IAppointmentsRepository
{
    public AppointmentsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Appointments?> GetByIdAsync(long id)
    {
        return await Query().FirstOrDefaultAsync(a => a.AppointmentId == id);
    }

    public override async Task<IEnumerable<Appointments>> GetAllAsync()
    {
        return await Query().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Appointments>> GetByPetIdAsync(long petId)
    {
        return await Query().AsNoTracking().Where(a => a.PetId == petId).ToListAsync();
    }

    public async Task<IEnumerable<Appointments>> GetByEmployeeIdAsync(long employeeId)
    {
        return await Query().AsNoTracking().Where(a => a.EmployeeId == employeeId).ToListAsync();
    }

    private IQueryable<Appointments> Query()
    {
        return Set
            .Include(a => a.Status)
            .Include(a => a.Room);
    }
}
