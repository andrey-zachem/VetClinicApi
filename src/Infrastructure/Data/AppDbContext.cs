using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> User => Set<User>();
    public DbSet<Role> Role => Set<Role>();
    public DbSet<UserRole> UserRole => Set<UserRole>();

    public DbSet<Clients> Clients => Set<Clients>();
    public DbSet<Pets> Pets => Set<Pets>();
    public DbSet<Breeds> Breeds => Set<Breeds>();
    public DbSet<Species> Species => Set<Species>();

    public DbSet<Employees> Employees => Set<Employees>();
    public DbSet<WorkSchedule> WorkSchedule => Set<WorkSchedule>();
    public DbSet<Positions> Positions => Set<Positions>();
    public DbSet<Specialties> Specialties => Set<Specialties>();
    public DbSet<EmployeeSpecialties> EmployeeSpecialties => Set<EmployeeSpecialties>();

    public DbSet<AppointmentStatuses> AppointmentStatuses => Set<AppointmentStatuses>();
    public DbSet<Appointments> Appointments => Set<Appointments>();
    public DbSet<Rooms> Rooms => Set<Rooms>();
    public DbSet<Visits> Visits => Set<Visits>();
    public DbSet<Diagnoses> Diagnoses => Set<Diagnoses>();
    public DbSet<VisitDiagnoses> VisitDiagnoses => Set<VisitDiagnoses>();
    public DbSet<AttachmentKinds> AttachmentKinds => Set<AttachmentKinds>();
    public DbSet<FileObjects> FileObjects => Set<FileObjects>();
    public DbSet<VisitAttachments> VisitAttachments => Set<VisitAttachments>();
    public DbSet<Medications> Medications => Set<Medications>();
    public DbSet<Prescriptions> Prescriptions => Set<Prescriptions>();
    public DbSet<Vaccines> Vaccines => Set<Vaccines>();
    public DbSet<VaccineBatches> VaccineBatches => Set<VaccineBatches>();
    public DbSet<Vaccinations> Vaccinations => Set<Vaccinations>();
    public DbSet<ServiceCategories> ServiceCategories => Set<ServiceCategories>();
    public DbSet<Services> Services => Set<Services>();
    public DbSet<ServicePrices> ServicePrices => Set<ServicePrices>();
    public DbSet<Invoices> Invoices => Set<Invoices>();
    public DbSet<InvoiceItems> InvoiceItems => Set<InvoiceItems>();
    public DbSet<PaymentMethods> PaymentMethods => Set<PaymentMethods>();
    public DbSet<Payments> Payments => Set<Payments>();
    public DbSet<AuditLog> AuditLog => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("vet");
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}
