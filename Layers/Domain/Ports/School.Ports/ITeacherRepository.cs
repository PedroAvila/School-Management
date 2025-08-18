using School.Entities;
using School.Entities.Extends;

namespace School.Ports;

public interface ITeacherRepository : IBaseRepository<Teacher>
{
    Task<Teacher> GetByIdAsync(int id);
    Task<int> CreateAsync(Teacher entity);

    Task<bool> UpdateAsync(Teacher entity);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<StudentTeacherSchool>> GetStudentsByTeacherAsync(int teacherId);
    Task<IEnumerable<StudentTeacherSchool>> GetTeacherSchoolsWithStudentsAsync(int teacherId);
}
