using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClientsConfiguration : IEntityTypeConfiguration<Clients>
{
    public void Configure(EntityTypeBuilder<Clients> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(c => c.ClientId);
        builder.Property(c => c.ClientId)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(c => c.LastName)
            .HasColumnName("last_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(c => c.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(c => c.MiddleName)
            .HasColumnName("middle_name")
            .HasColumnType("text");

        builder.Property(c => c.PhoneNumber)
            .HasColumnName("phone")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("email");

        builder.Property(c => c.Address)
            .HasColumnName("address")
            .HasColumnType("text");

        builder.Property(c => c.Note)
            .HasColumnName("note")
            .HasColumnType("text");

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(c => c.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.UserId)
            .HasColumnName("user_id");

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.HasIndex(c => c.PhoneNumber)
            .IsUnique();

        builder.HasIndex(c => c.UserId)
            .IsUnique();

        builder.HasOne(c => c.User)
            .WithOne(u => u.Client)
            .HasForeignKey<Clients>(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
