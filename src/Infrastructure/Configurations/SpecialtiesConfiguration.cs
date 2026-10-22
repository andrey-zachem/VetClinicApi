using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SpecialtiesConfiguration : IEntityTypeConfiguration<Specialties>
{
    public void Configure(EntityTypeBuilder<Specialties> builder)
    {
        builder.ToTable("specialties");

        builder.HasKey(s => s.SpecialtyId);

        builder.Property(s => s.SpecialtyId)
            .HasColumnName("specialty_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnType("text")
            .HasColumnName("name");

        builder.HasIndex(s => s.Name)
            .IsUnique();
    }
}
