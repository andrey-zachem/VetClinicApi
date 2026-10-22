using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class RoomsConfiguration : IEntityTypeConfiguration<Rooms>
{
    public void Configure(EntityTypeBuilder<Rooms> builder)
    {
        builder.ToTable("rooms");
        builder.HasKey(x => x.RoomId);
        builder.Property(x => x.RoomId)
            .HasColumnName("room_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
