using CareFlow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareFlow.Configurations;

public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.Property(a => a.Date).HasColumnType("date");
        builder.Property(a => a.Status).HasConversion<string>();
    }
}
