using System.Linq.Expressions;
using Fitness.Common.Types;
using MongoDB.Driver;

namespace Fitness.Common.Mongo;

public class MongoRepository<T> : IMongoRepository<T> where T : IIdentifier
{
    public IMongoCollection<T> Collection { get; }

    public MongoRepository(MongoContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var database = context.Database;
        Collection = database.GetCollection<T>(context.CollectionName);
    }
    
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await Collection.Find(predicate).SingleOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
    }
    
}