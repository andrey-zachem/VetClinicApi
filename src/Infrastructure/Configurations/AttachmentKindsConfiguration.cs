using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AttachmentKindsConfiguration : IEntityTypeConfiguration<AttachmentKinds>
{
    public void Configure(EntityTypeBuilder<AttachmentKinds> builder)
    {
        builder.ToTable("attachment_kinds");
        builder.HasKey(x => x.KindId);
        builder.Property(x => x.KindId)
            .HasColumnName("kind_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
