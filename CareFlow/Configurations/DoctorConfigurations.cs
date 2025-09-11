using CareFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareFlow.Configurations;

public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasMany(d => d.Appointments)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Consultations)
               .WithOne(c => c.Doctor)
               .HasForeignKey(c => c.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
