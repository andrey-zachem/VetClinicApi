using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VaccinationsConfiguration : IEntityTypeConfiguration<Vaccinations>
{
    public void Configure(EntityTypeBuilder<Vaccinations> builder)
    {
        builder.ToTable("vaccinations");

        builder.HasKey(x => x.VaccinationId);
        builder.Property(x => x.VaccinationId)
            .HasColumnName("vaccination_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.PetId).HasColumnName("pet_id").IsRequired();
        builder.Property(x => x.BatchId).HasColumnName("batch_id").IsRequired();
        builder.Property(x => x.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(x => x.VisitId).HasColumnName("visit_id");
        builder.Property(x => x.VaccinatedOn)
            .HasColumnName("vaccinated_on")
            .HasColumnType("date")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_DATE");
        builder.Property(x => x.Note).HasColumnName("note");

        builder.HasOne(x => x.Pet)
            .WithMany(p => p.Vaccinations)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Batch)
            .WithMany(b => b.Vaccinations)
            .HasForeignKey(x => x.BatchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Vaccinations)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Visit)
            .WithMany(v => v.Vaccinations)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
