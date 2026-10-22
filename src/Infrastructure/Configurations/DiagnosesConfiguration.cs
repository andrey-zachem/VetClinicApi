using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class DiagnosesConfiguration : IEntityTypeConfiguration<Diagnoses>
{
    public void Configure(EntityTypeBuilder<Diagnoses> builder)
    {
        builder.ToTable("diagnoses");
        builder.HasKey(x => x.DiagnosisId);
        builder.Property(x => x.DiagnosisId)
            .HasColumnName("diagnosis_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
