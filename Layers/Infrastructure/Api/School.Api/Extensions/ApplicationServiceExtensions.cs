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
        services.AddScoped<ISchoolRepository>(provider => new SchoolRepository(connectionString));
        services.AddScoped<ITeacherRepository>(provider => new TeacherRepository(connectionString));
        services.AddScoped<IStudentRepository>(provider => new StudentRepository(connectionString));
        services.AddScoped<IRegistrationRepository>(provider => new RegistrationRepository(
            connectionString
        ));
    }
}
