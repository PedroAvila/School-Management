using School.Adapters;
using School.Ports;

namespace School.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        string connectionString
    )
    {
        // services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ISchoolRepository>(provider => new SchoolRepository(connectionString));
    }
}
