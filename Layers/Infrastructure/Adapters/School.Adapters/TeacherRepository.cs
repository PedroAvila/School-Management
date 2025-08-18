using System.Data;
using Dapper;
using School.Adapters.Enums;
using School.Entities;
using School.Entities.Extends;
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

    public async Task<IEnumerable<StudentTeacherSchool>> GetStudentsByTeacherAsync(int teacherId)
    {
        var sql =
            @"
        SELECT 
            CONCAT(s.FirstName, ' ', s.LastName) AS Student,
            CONCAT(tch.FirstName, ' ', tch.LastName) AS Teacher,
            school.Name AS School  
        FROM 
            Students s
            INNER JOIN Teachers_Students ts 
                ON s.Id = ts.StudentId
            INNER JOIN Teachers tch 
                ON tch.Id = ts.TeacherId
            INNER JOIN Registrations r 
                ON r.StudentId = ts.StudentId
            INNER JOIN Schools school 
                ON school.Id = r.SchoolId
        WHERE 
            ts.TeacherId = @TeacherId";

        using var connection = CreateConnection();

        return await connection.QueryAsync<StudentTeacherSchool>(
            sql,
            new { TeacherId = teacherId }
        );
    }

    public async Task<IEnumerable<StudentTeacherSchool>> GetTeacherSchoolsWithStudentsAsync(
        int teacherId
    )
    {
        const string sql =
            @"
        SELECT 
            s.Name AS School,
            CONCAT(t.FirstName, ' ', t.LastName) AS Teacher,
            CONCAT(st.FirstName, ' ', st.LastName) AS Student
        FROM 
            Teachers t
            INNER JOIN Schools s ON t.SchoolId = s.Id
            INNER JOIN Teachers_Students ts ON t.Id = ts.TeacherId
            INNER JOIN Students st ON st.Id = ts.StudentId
            INNER JOIN Registrations r ON r.StudentId = st.Id AND r.SchoolId = s.Id
        WHERE 
            t.Id = @teacherId
        ORDER BY 
            s.Name, st.LastName, st.FirstName";

        using var connection = CreateConnection();
        return await connection.QueryAsync<StudentTeacherSchool>(sql, new { teacherId });
    }
}
