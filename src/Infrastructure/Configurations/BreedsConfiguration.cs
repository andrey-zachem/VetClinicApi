using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class BreedsConfiguration : IEntityTypeConfiguration<Breeds>
{
    public void Configure(EntityTypeBuilder<Breeds> builder)
    {
        builder.ToTable("breeds");
        builder.HasKey(b => b.BreedsId);
        builder.Property(b => b.BreedsId)
            .HasColumnName("breed_id");

        builder.Property(b => b.SpeciesId)
            .HasColumnName("species_id")
            .IsRequired();

        builder.Property(b => b.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.HasIndex(b => new { b.SpeciesId, b.Name }).IsUnique();

        builder.HasOne(b => b.Species)
            .WithMany(s => s.Breeds)
            .HasForeignKey(b => b.SpeciesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}