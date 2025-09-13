using CareFlow.Data;
using CareFlow.Services;
using CareFlow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CareFlow.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>
            (options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
        return services;
    }

    public static IServiceCollection AddDependencyInjectionContainer(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<ISpecialtyService, SpecialtyService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IConsultationService, ConsultationService>();
        return services;
    }
}
