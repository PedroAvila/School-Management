using School.Entities;

namespace School.Ports;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    Task<Teacher> GetByIdAsync(int id);
    Task<Teacher> CreateAsync(Teacher entity);

    Task<Teacher> UpdateAsync(Teacher entity);

    Task DeleteAsync(int id);
}
