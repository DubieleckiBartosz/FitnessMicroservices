using System.Linq.Expressions;
using Fitness.Common.Types;
using MongoDB.Driver;

namespace Fitness.Common.Mongo;

public class MongoRepository<T> : IMongoRepository<T> where T : IIdentifier
{
    protected IMongoCollection<T> Collection { get; }

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).SingleOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(string id)
    {
        await Collection.DeleteOneAsync(e => e.Id == id);
    }
}