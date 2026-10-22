using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VaccineBatchesConfiguration : IEntityTypeConfiguration<VaccineBatches>
{
    public void Configure(EntityTypeBuilder<VaccineBatches> builder)
    {
        builder.ToTable("vaccine_batches");

        builder.HasKey(x => x.BatchId);
        builder.Property(x => x.BatchId)
            .HasColumnName("batch_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.VaccineId).HasColumnName("vaccine_id").IsRequired();
        builder.Property(x => x.BatchNo).HasColumnName("batch_no").IsRequired();
        builder.Property(x => x.ExpiresOn).HasColumnName("expires_on").HasColumnType("date").IsRequired();

        builder.HasIndex(x => new { x.VaccineId, x.BatchNo }).IsUnique();

        builder.HasOne(x => x.Vaccine)
            .WithMany(v => v.Batches)
            .HasForeignKey(x => x.VaccineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
