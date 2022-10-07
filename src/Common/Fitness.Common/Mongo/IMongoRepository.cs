using Fitness.Common.Types;
using System.Linq.Expressions;

namespace Fitness.Common.Mongo;

public interface IMongoRepository<T> where T : IIdentifier
{
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task DeleteAsync(string id);
}