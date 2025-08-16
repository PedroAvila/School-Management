namespace School.Ports;

public interface ISchoolRepository : IBaseRepository<Entities.School>
{
    Task<Entities.School> GetByIdAsync(int id);
    Task<int> CreateAsync(Entities.School entity);

    Task<bool> UpdateAsync(Entities.School entity);

    Task<bool> DeleteAsync(int id);
}
