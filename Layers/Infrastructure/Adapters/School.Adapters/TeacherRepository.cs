using System.Data;
using Dapper;
using School.Adapters.Enums;
using School.Entities;
using School.Ports;

namespace School.Adapters;

public class TeacherRepository(string connectionString)
    : BaseRepository<Teacher>(connectionString),
        ITeacherRepository
{
    public async Task<Teacher> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        var teacher = await connection.QueryFirstAsync<Teacher>(
            "sp_Teacher_CRUD",
            new { Accion = OperationCrud.GET_BY_ID, Id = id },
            commandType: CommandType.StoredProcedure
        );
        return teacher;
    }

    public async Task<int> CreateAsync(Teacher entity)
    {
        using var connection = CreateConnection();
        var result = await connection.QueryFirstAsync<int>(
            "sp_Teacher_CRUD",
            new
            {
                Accion = OperationCrud.CREATE,
                SchoolId = entity.SchoolId,
                Code = entity.Code,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            },
            commandType: CommandType.StoredProcedure
        );
        return result;
    }

    public async Task<bool> UpdateAsync(Teacher entity)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_Teacher_CRUD",
            new
            {
                Accion = OperationCrud.UPDATE,
                Id = entity.Id,
                SchoolId = entity.SchoolId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            },
            commandType: CommandType.StoredProcedure
        );
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_Teacher_CRUD",
            new { Accion = OperationCrud.DELETE, Id = id },
            commandType: CommandType.StoredProcedure
        );
        return rowsAffected > 0;
    }
}
