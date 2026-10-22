using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class EmployeeSpecialtiesConfiguration : IEntityTypeConfiguration<EmployeeSpecialties>
{
    public void Configure(EntityTypeBuilder<EmployeeSpecialties> builder)
    {
        builder.ToTable("employee_specialties");

        builder.HasKey(es => new { es.EmployeeId, es.SpecialtyId });

        builder.Property(es => es.EmployeeId)
            .HasColumnName("employee_id");

        builder.Property(es => es.SpecialtyId)
            .HasColumnName("specialty_id");

        builder.HasOne(es => es.Employee)
            .WithMany(e => e.EmployeeSpecialties)
            .HasForeignKey(es => es.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(es => es.Specialty)
            .WithMany(s => s.EmployeeSpecialties)
            .HasForeignKey(es => es.SpecialtyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
