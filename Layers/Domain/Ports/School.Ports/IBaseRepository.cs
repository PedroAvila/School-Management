using System.Linq.Expressions;

namespace School.Ports;

public interface IBaseRepository<T>
    where T : class
{
    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
    Task<int> GenerateCodeAsync();
}
