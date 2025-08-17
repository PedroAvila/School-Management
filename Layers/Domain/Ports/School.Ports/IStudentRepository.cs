using School.Entities;

namespace School.Ports;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<Student> GetByIdAsync(int id);
    Task<int> CreateAsync(Student entity);

    Task<bool> UpdateAsync(Student entity);

    Task<bool> DeleteAsync(int id);
}
