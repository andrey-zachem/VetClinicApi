using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PaymentsConfiguration : IEntityTypeConfiguration<Payments>
{
    public void Configure(EntityTypeBuilder<Payments> builder)
    {
        builder.ToTable("payments");

        builder.HasKey(x => x.PaymentId);
        builder.Property(x => x.PaymentId)
            .HasColumnName("payment_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.InvoiceId).HasColumnName("invoice_id").IsRequired();
        builder.Property(x => x.MethodId).HasColumnName("method_id").IsRequired();
        builder.Property(x => x.Amount).HasColumnName("amount").HasColumnType("numeric(12,2)").IsRequired();
        builder.Property(x => x.PaidAt)
            .HasColumnName("paid_at")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasOne(x => x.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Method)
            .WithMany(m => m.Payments)
            .HasForeignKey(x => x.MethodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
