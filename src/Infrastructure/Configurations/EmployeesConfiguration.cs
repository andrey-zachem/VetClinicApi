using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
{
    public void Configure(EntityTypeBuilder<Employees> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.EmployeeId)
            .HasColumnName("employee_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id");

        builder.HasIndex(e => e.UserId)
            .IsUnique();

        builder.Property(e => e.PositionId)
            .HasColumnName("position_id")
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(u => u.Employees)
            .HasForeignKey(e => e.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.Position)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasColumnType("text")
            .HasColumnName("first_name");

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasColumnType("text")
            .HasColumnName("last_name");

        builder.Property(e => e.MiddleName)
            .HasColumnType("text")
            .HasColumnName("middle_name");

        builder.Property(e => e.Email)
            .HasColumnType("text")
            .HasColumnName("email");

        builder.Property(e => e.Phone)
            .HasColumnType("text")
            .HasColumnName("phone");

        builder.Property(e => e.HiredOn)
            .IsRequired()
            .HasColumnType("date")
            .HasColumnName("hired_on");

        builder.Property(e => e.FiredOn)
            .HasColumnType("date")
            .HasColumnName("fired_on");
    }
}
