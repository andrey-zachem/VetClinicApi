using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.ToTable("work_schedules");

        builder.HasKey(ws => ws.ScheduleId);

        builder.Property(ws => ws.ScheduleId)
            .HasColumnName("schedule_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(ws => ws.EmployeeId)
            .HasColumnName("employee_id")
            .IsRequired();

        builder.Property(ws => ws.WeekDay)
            .HasColumnName("weekday")
            .IsRequired();

        builder.Property(ws => ws.StartsAt)
            .HasColumnName("starts_at")
            .HasColumnType("time")
            .IsRequired();

        builder.Property(ws => ws.EndsAt)
            .HasColumnName("ends_at")
            .HasColumnType("time")
            .IsRequired();

        builder.HasOne(ws => ws.Employee)
            .WithMany(e => e.WorkSchedules)
            .HasForeignKey(ws => ws.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ws => new { ws.EmployeeId, ws.WeekDay, ws.StartsAt })
            .IsUnique();
    }
}
