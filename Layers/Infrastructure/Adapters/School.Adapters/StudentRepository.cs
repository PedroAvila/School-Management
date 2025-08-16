using School.Entities;
using School.Ports;

namespace School.Adapters;

public class StudentRepository(string connectionString)
    : BaseRepository<Student>(connectionString),
        IStudentRepository
{
    public Task<Student> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> CreateAsync(Student entity)
    {
        throw new NotImplementedException();
    }

    public Task<Student> UpdateAsync(Student entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
