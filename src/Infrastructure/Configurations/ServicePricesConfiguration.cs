using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServicePricesConfiguration : IEntityTypeConfiguration<ServicePrices>
{
    public void Configure(EntityTypeBuilder<ServicePrices> builder)
    {
        builder.ToTable("service_prices");

        builder.HasKey(x => x.PriceId);
        builder.Property(x => x.PriceId)
            .HasColumnName("price_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.ServiceId).HasColumnName("service_id").IsRequired();
        builder.Property(x => x.Price).HasColumnName("price").HasColumnType("numeric(12,2)").IsRequired();
        builder.Property(x => x.ValidFrom).HasColumnName("valid_from").HasColumnType("date").IsRequired();

        builder.HasIndex(x => new { x.ServiceId, x.ValidFrom }).IsUnique();

        builder.HasOne(x => x.Service)
            .WithMany(s => s.Prices)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
