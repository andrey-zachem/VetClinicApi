using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VisitDiagnosesConfiguration : IEntityTypeConfiguration<VisitDiagnoses>
{
    public void Configure(EntityTypeBuilder<VisitDiagnoses> builder)
    {
        builder.ToTable("visit_diagnoses");
        builder.HasKey(x => new { x.VisitId, x.DiagnosisId });

        builder.Property(x => x.VisitId).HasColumnName("visit_id");
        builder.Property(x => x.DiagnosisId).HasColumnName("diagnosis_id");
        builder.Property(x => x.IsPrimary)
            .HasColumnName("is_primary")
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(x => x.Visit)
            .WithMany(v => v.Diagnoses)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Diagnosis)
            .WithMany(d => d.VisitDiagnoses)
            .HasForeignKey(x => x.DiagnosisId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
