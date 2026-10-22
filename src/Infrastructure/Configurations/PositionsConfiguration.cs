using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PositionsConfiguration : IEntityTypeConfiguration<Positions>
{
    public void Configure(EntityTypeBuilder<Positions> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(p => p.PositionId);

        builder.Property(p => p.PositionId)
            .HasColumnName("position_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("text")
            .HasColumnName("name");

        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
