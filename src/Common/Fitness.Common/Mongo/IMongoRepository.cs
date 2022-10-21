using Fitness.Common.Types;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Fitness.Common.Mongo;

public interface IMongoRepository<T> where T : IIdentifier
{
    IMongoCollection<T> Collection { get; }
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
}