using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        builder.HasKey(s => s.SpeciesId);
        builder.Property(s => s.SpeciesId)
            .HasColumnName("species_id");
        builder.Property(s => s.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.HasIndex(s => s.Name).IsUnique();
    }
}