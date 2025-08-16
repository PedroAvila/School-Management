using System.Data;
using Dapper;
using School.Adapters.Enums;
using School.Ports;

namespace School.Adapters;

public class SchoolRepository(string connectionString)
    : BaseRepository<Entities.School>(connectionString),
        ISchoolRepository
{
    public async Task<Entities.School> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        var school = await connection.QueryFirstAsync<Entities.School>(
            "sp_School_CRUD",
            new { Accion = OperationCrud.GET_BY_ID, Id = id },
            commandType: CommandType.StoredProcedure
        );
        return school;
    }

    public async Task<int> CreateAsync(Entities.School entity)
    {
        using var connection = CreateConnection();
        var result = await connection.QueryFirstAsync<int>(
            "sp_School_CRUD",
            new
            {
                Accion = OperationCrud.CREATE,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
            },
            commandType: CommandType.StoredProcedure
        );
        return result;
    }

    public async Task<bool> UpdateAsync(Entities.School entity)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_School_CRUD",
            new
            {
                Accion = OperationCrud.UPDATE,
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            },
            commandType: CommandType.StoredProcedure
        );
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_School_CRUD",
            new { Accion = OperationCrud.DELETE, Id = id },
            commandType: CommandType.StoredProcedure
        );

        return rowsAffected > 0;
    }
}
