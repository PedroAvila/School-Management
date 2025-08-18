using System.Data;
using Dapper;
using School.Adapters.Enums;
using School.Entities;
using School.Ports;

namespace School.Adapters;

public class StudentRepository(string connectionString)
    : BaseRepository<Student>(connectionString),
        IStudentRepository
{
    public async Task<Student> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        var student = await connection.QueryFirstAsync<Student>(
            "sp_Student_CRUD",
            new { Accion = OperationCrud.GET_BY_ID, Id = id },
            commandType: CommandType.StoredProcedure
        );
        return student;
    }

    public async Task<int> CreateAsync(Student entity)
    {
        using var connection = CreateConnection();
        var result = await connection.QueryFirstAsync<int>(
            "sp_Student_CRUD",
            new
            {
                Accion = OperationCrud.CREATE,
                Code = entity.Code,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
            },
            commandType: CommandType.StoredProcedure
        );
        return result;
    }

    public async Task<bool> UpdateAsync(Student entity)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_Student_CRUD",
            new
            {
                Accion = OperationCrud.UPDATE,
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
            },
            commandType: CommandType.StoredProcedure
        );
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = CreateConnection();
        var rowsAffected = await connection.ExecuteAsync(
            "sp_Student_CRUD",
            new { Accion = OperationCrud.DELETE, Id = id },
            commandType: CommandType.StoredProcedure
        );
        return rowsAffected > 0;
    }

    public async Task<bool> AssignStudentToTeacherAsync(int studentId, int teacherId)
    {
        using var connection = CreateConnection();
        var result = await connection.ExecuteScalarAsync<int>(
            @"IF NOT EXISTS (
              SELECT 1 FROM Teachers_Students 
              WHERE TeacherId = @TeacherId AND StudentId = @StudentId
          )
          BEGIN
              INSERT INTO Teachers_Students (TeacherId, StudentId)
              VALUES (@TeacherId, @StudentId);
              SELECT 1;
          END
          ELSE
              SELECT 0;",
            new { TeacherId = teacherId, StudentId = studentId }
        );

        return result > 0;
    }
}
