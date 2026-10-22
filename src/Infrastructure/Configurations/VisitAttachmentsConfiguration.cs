using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VisitAttachmentsConfiguration : IEntityTypeConfiguration<VisitAttachments>
{
    public void Configure(EntityTypeBuilder<VisitAttachments> builder)
    {
        builder.ToTable("visit_attachments");
        builder.HasKey(x => new { x.VisitId, x.FileId });

        builder.Property(x => x.VisitId).HasColumnName("visit_id");
        builder.Property(x => x.FileId).HasColumnName("file_id");
        builder.Property(x => x.KindId).HasColumnName("kind_id").IsRequired();
        builder.Property(x => x.Comment).HasColumnName("comment");

        builder.HasOne(x => x.Visit)
            .WithMany(v => v.Attachments)
            .HasForeignKey(x => x.VisitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.File)
            .WithMany(f => f.VisitAttachments)
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Kind)
            .WithMany(k => k.VisitAttachments)
            .HasForeignKey(x => x.KindId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
