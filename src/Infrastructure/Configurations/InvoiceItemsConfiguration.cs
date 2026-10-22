using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class InvoiceItemsConfiguration : IEntityTypeConfiguration<InvoiceItems>
{
    public void Configure(EntityTypeBuilder<InvoiceItems> builder)
    {
        builder.ToTable("invoice_items");

        builder.HasKey(x => x.ItemId);
        builder.Property(x => x.ItemId)
            .HasColumnName("item_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.InvoiceId).HasColumnName("invoice_id").IsRequired();
        builder.Property(x => x.ServiceId).HasColumnName("service_id").IsRequired();
        builder.Property(x => x.EmployeeId).HasColumnName("employee_id");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasColumnType("numeric(8,2)").IsRequired();
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasColumnType("numeric(12,2)").IsRequired();
        builder.Property(x => x.Discount)
            .HasColumnName("discount")
            .HasColumnType("numeric(12,2)")
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(x => x.Invoice)
            .WithMany(i => i.Items)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Service)
            .WithMany(s => s.InvoiceItems)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.InvoiceItems)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
