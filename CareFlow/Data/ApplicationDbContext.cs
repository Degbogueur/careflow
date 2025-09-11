using CareFlow.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CareFlow.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Consultation> Consultations { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
}
