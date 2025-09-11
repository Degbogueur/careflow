using CareFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareFlow.Configurations;

public class PatientConfigurations : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(p => p.DateOfBirth).HasColumnType("date");
        builder.Property(p => p.Gender).HasConversion<string>();

        builder.HasOne(p => p.MedicalRecord)
               .WithOne(m => m.Patient)
               .HasForeignKey<Patient>(p => p.MedicalRecordId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Appointments)
               .WithOne(a => a.Patient)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(p => p.Address, a =>
        {
            a.Property(a => a.Street).HasColumnName("Street");
            a.Property(a => a.City).HasColumnName("City");
            a.Property(a => a.Province).HasColumnName("Province");
            a.Property(a => a.Country).HasColumnName("Country");
            a.Property(a => a.PostalCode).HasColumnName("PostalCode");
        });
    }
}
