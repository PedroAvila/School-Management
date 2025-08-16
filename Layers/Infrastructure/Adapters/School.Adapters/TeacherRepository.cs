using School.Entities;
using School.Ports;

namespace School.Adapters;

public class TeacherRepository(string connectionString)
    : BaseRepository<Teacher>(connectionString),
        ITeacherRepository
{
    public Task<Teacher> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();
        throw new NotImplementedException();
    }

    public Task<Teacher> CreateAsync(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task<Teacher> UpdateAsync(Teacher entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
