using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MedicationsConfiguration : IEntityTypeConfiguration<Medications>
{
    public void Configure(EntityTypeBuilder<Medications> builder)
    {
        builder.ToTable("medications");
        builder.HasKey(x => x.MedicationId);
        builder.Property(x => x.MedicationId)
            .HasColumnName("medication_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Form).HasColumnName("form");
        builder.Property(x => x.ActiveSubstance).HasColumnName("active_substance");
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
