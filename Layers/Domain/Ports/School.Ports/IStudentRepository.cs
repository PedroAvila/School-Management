using School.Entities;

namespace School.Ports;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<Student> GetByIdAsync(int id);
    Task<Student> CreateAsync(Student entity);

    Task<Student> UpdateAsync(Student entity);

    Task DeleteAsync(int id);
}
