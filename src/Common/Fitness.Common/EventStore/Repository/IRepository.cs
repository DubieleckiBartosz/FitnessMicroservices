using Fitness.Common.EventStore.Aggregate; 

namespace Fitness.Common.EventStore.Repository;

public interface IRepository<TAggregate> where TAggregate : AggregateRoot
{
    Task<TAggregate> GetAsync(Guid id); 
    Task AddAsync(TAggregate aggregate); 
    Task UpdateAsync(TAggregate aggregate); 
    Task DeleteAsync(TAggregate aggregate); 
}