using Fitness.Common.EventStore.Aggregate; 

namespace Fitness.Common.EventStore.Repository;

public interface IRepository<TAggregate> where TAggregate : Aggregate.Aggregate
{
    Task<TAggregate> GetAsync(Guid id);
    Task AddAsync(TAggregate aggregate);
    Task AddAndPublishAsync(TAggregate aggregate);
    Task UpdateAsync(TAggregate aggregate);
    Task UpdateAndPublishAsync(TAggregate aggregate);
    Task DeleteAsync(TAggregate aggregate);
    Task DeleteAndPublishAsync(TAggregate aggregate); 
}