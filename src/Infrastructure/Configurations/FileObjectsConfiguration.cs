using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class FileObjectsConfiguration : IEntityTypeConfiguration<FileObjects>
{
    public void Configure(EntityTypeBuilder<FileObjects> builder)
    {
        builder.ToTable("file_objects");

        builder.HasKey(x => x.FileId);
        builder.Property(x => x.FileId)
            .HasColumnName("file_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.Bucket).HasColumnName("bucket").IsRequired();
        builder.Property(x => x.ObjectKey).HasColumnName("object_key").IsRequired();
        builder.Property(x => x.OriginalName).HasColumnName("original_name").IsRequired();
        builder.Property(x => x.ContentType).HasColumnName("content_type").IsRequired();
        builder.Property(x => x.SizeBytes).HasColumnName("size_bytes").IsRequired();
        builder.Property(x => x.Etag).HasColumnName("etag");
        builder.Property(x => x.UploadedBy).HasColumnName("uploaded_by");
        builder.Property(x => x.UploadedAt)
            .HasColumnName("uploaded_at")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasIndex(x => new { x.Bucket, x.ObjectKey }).IsUnique();

        builder.HasOne(x => x.UploadedByUser)
            .WithMany(u => u.UploadedFiles)
            .HasForeignKey(x => x.UploadedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
