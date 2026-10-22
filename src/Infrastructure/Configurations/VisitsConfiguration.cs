using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class VisitsConfiguration : IEntityTypeConfiguration<Visits>
{
    public void Configure(EntityTypeBuilder<Visits> builder)
    {
        builder.ToTable("visits");

        builder.HasKey(x => x.VisitId);
        builder.Property(x => x.VisitId)
            .HasColumnName("visit_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.AppointmentId).HasColumnName("appointment_id");
        builder.Property(x => x.PetId).HasColumnName("pet_id").IsRequired();
        builder.Property(x => x.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(x => x.VisitedAt)
            .HasColumnName("visited_at")
            .IsRequired()
            .HasDefaultValueSql("now()");
        builder.Property(x => x.WeightKg).HasColumnName("weight_kg").HasColumnType("numeric(6,3)");
        builder.Property(x => x.TemperatureC).HasColumnName("temperature_c").HasColumnType("numeric(4,1)");
        builder.Property(x => x.Complaints).HasColumnName("complaints");
        builder.Property(x => x.Anamnesis).HasColumnName("anamnesis");
        builder.Property(x => x.Examination).HasColumnName("examination");
        builder.Property(x => x.TreatmentPlan).HasColumnName("treatment_plan");
        builder.Property(x => x.Recommendations).HasColumnName("recommendations");

        builder.HasIndex(x => x.AppointmentId).IsUnique();

        builder.HasOne(x => x.Appointment)
            .WithOne(a => a.Visit)
            .HasForeignKey<Visits>(x => x.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Pet)
            .WithMany(p => p.Visits)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Employee)
            .WithMany(e => e.Visits)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
