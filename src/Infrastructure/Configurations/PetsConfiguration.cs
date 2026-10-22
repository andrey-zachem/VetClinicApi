using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PetsConfiguration : IEntityTypeConfiguration<Pets>
{
    public void Configure(EntityTypeBuilder<Pets> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.PetId);
        builder.Property(p => p.PetId)
            .HasColumnName("pet_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(p => p.BreedId)
            .HasColumnName("breed_id")
            .IsRequired();

        builder.HasOne(p => p.Breeds)
            .WithMany(b => b.Pets)
            .HasForeignKey(p => p.BreedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.ClientId)
            .HasColumnName("client_id")
            .IsRequired();

        builder.HasOne(p => p.Client)
            .WithMany(c => c.Pets)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(p => p.Sex)
            .HasColumnName("sex")
            .HasColumnType("vet.pet_sex")
            .HasConversion(
                v => v.ToString().ToLowerInvariant(),
                v => Enum.Parse<PetSex>(v, true))
            .IsRequired()
            .HasDefaultValue(PetSex.Unknown);

        builder.Property(p => p.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("date");

        builder.Property(p => p.IsNeutered)
            .HasColumnName("is_neutered")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.Color)
            .HasColumnName("color");

        builder.Property(p => p.MicrochipNumber)
            .HasColumnName("microchip_no");

        builder.HasIndex(p => p.MicrochipNumber)
            .IsUnique();

        builder.Property(p => p.PhotoFileId)
            .HasColumnName("photo_file_id");

        builder.HasOne(p => p.Photo)
            .WithMany(f => f.Pets)
            .HasForeignKey(p => p.PhotoFileId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(p => p.DeceasedOn)
            .HasColumnName("deceased_on")
            .HasColumnType("date");

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("now()");
    }
}
