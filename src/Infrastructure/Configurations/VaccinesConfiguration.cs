using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VaccinesConfiguration : IEntityTypeConfiguration<Vaccines>
{
    public void Configure(EntityTypeBuilder<Vaccines> builder)
    {
        builder.ToTable("vaccines");
        builder.HasKey(x => x.VaccineId);
        builder.Property(x => x.VaccineId)
            .HasColumnName("vaccine_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Manufacturer).HasColumnName("manufacturer");
        builder.Property(x => x.ProtectsAgainst).HasColumnName("protects_against").IsRequired();
        builder.Property(x => x.ValidityDays).HasColumnName("validity_days").IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
