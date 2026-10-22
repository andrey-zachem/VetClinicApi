using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AppointmentStatusesConfiguration : IEntityTypeConfiguration<AppointmentStatuses>
{
    public void Configure(EntityTypeBuilder<AppointmentStatuses> builder)
    {
        builder.ToTable("appointment_statuses");
        builder.HasKey(x => x.StatusId);
        builder.Property(x => x.StatusId)
            .HasColumnName("status_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
