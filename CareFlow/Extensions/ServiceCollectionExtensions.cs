using CareFlow.BackgroundJobs;
using CareFlow.BackgroundJobs.Interfaces;
using CareFlow.Data;
using CareFlow.Helpers;
using CareFlow.Models.Identity;
using CareFlow.Services;
using CareFlow.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Identity;
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

    public static IServiceCollection AddServicesDependencyInjectionContainer(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<ISpecialtyService, SpecialtyService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IConsultationService, ConsultationService>();
        return services;
    }

    public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        return services;
    }

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SuperAdminSettings>(configuration.GetSection("SuperAdminSettings"));
        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("DbConnection")));
        services.AddHangfireServer();

        services.AddScoped<IUserAccountBackgroundJobs, UserAccountBackgroundJobs>();

        return services;
    }
}
