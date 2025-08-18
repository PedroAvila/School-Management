using Dapper;
using School.Entities;
using School.Ports;

namespace School.Adapters;

public class RegistrationRepository(string connectionString)
    : BaseRepository<Registration>(connectionString),
        IRegistrationRepository
{
    public async Task<int> CreateAsync(Registration entity)
    {
        const string sql =
            @"
        INSERT INTO Registrations 
            (Code, StudentId, SchoolId, RegistrationDate)
        VALUES 
            (@Code, @StudentId, @SchoolId, @RegistrationDate);
        
        SELECT CAST(SCOPE_IDENTITY() as int);";

        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, entity);
    }
}
