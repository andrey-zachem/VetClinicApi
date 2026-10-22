using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class VisitsRepository : CrudRepository<Visits, long>, IVisitsRepository
{
    public VisitsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Visits?> GetByIdAsync(long id)
    {
        return await Query().FirstOrDefaultAsync(v => v.VisitId == id);
    }

    public override async Task<IEnumerable<Visits>> GetAllAsync()
    {
        return await Query().AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Visits>> GetByPetIdAsync(long petId)
    {
        return await Query().AsNoTracking().Where(v => v.PetId == petId).ToListAsync();
    }

    public async Task ReplaceDiagnosesAsync(long visitId, IEnumerable<(int DiagnosisId, bool IsPrimary)> diagnoses)
    {
        var existing = Context.VisitDiagnoses.Where(d => d.VisitId == visitId);
        Context.VisitDiagnoses.RemoveRange(existing);

        foreach (var (diagnosisId, isPrimary) in diagnoses.DistinctBy(x => x.DiagnosisId))
        {
            Context.VisitDiagnoses.Add(new VisitDiagnoses
            {
                VisitId = visitId,
                DiagnosisId = diagnosisId,
                IsPrimary = isPrimary
            });
        }

        await Context.SaveChangesAsync();
    }

    private IQueryable<Visits> Query()
    {
        return Set
            .AsSplitQuery()
            .Include(v => v.Diagnoses)
                .ThenInclude(d => d.Diagnosis)
            .Include(v => v.Prescriptions)
            .Include(v => v.Attachments);
    }
}
