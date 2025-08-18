using School.Entities;

namespace School.Ports;

public interface IRegistrationRepository : IBaseRepository<Registration>
{
    Task<int> CreateAsync(Registration entity);
}
