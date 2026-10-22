using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PrescriptionsConfiguration : IEntityTypeConfiguration<Prescriptions>
{
    public void Configure(EntityTypeBuilder<Prescriptions> builder)
    {
        builder.ToTable("prescriptions");

        builder.HasKey(x => x.PrescriptionId);
        builder.Property(x => x.PrescriptionId)
            .HasColumnName("prescription_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.VisitId).HasColumnName("visit_id").IsRequired();
        builder.Property(x => x.MedicationId).HasColumnName("medication_id").IsRequired();
        builder.Property(x => x.Dosage).HasColumnName("dosage").IsRequired();
        builder.Property(x => x.Frequency).HasColumnName("frequency");
        builder.Property(x => x.DurationDays).HasColumnName("duration_days");
        builder.Property(x => x.Instructions).HasColumnName("instructions");

        builder.HasOne(x => x.Visit)
            .WithMany(v => v.Prescriptions)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Medication)
            .WithMany(m => m.Prescriptions)
            .HasForeignKey(x => x.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
