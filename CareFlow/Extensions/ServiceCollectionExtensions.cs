using CareFlow.Data;
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
}
