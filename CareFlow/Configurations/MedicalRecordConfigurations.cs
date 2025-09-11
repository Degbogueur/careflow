using CareFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareFlow.Configurations;

public class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.Property(r => r.BloodType).HasConversion<string>();

        builder.HasMany(r => r.Consultations)
            .WithOne(c => c.MedicalRecord)
            .HasForeignKey(c => c.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
