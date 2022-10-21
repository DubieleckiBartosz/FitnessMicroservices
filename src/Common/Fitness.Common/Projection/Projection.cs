using System.Runtime.CompilerServices;
using Fitness.Common.EventStore.Events;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Common.Projection;

public abstract class Projection : IProjection
{
    private readonly Dictionary<Type, Func<IEvent, CancellationToken, Task>> _handlers = new();


    public Type[] Handles => _handlers.Keys.ToArray();

    protected void Projects<TEvent>(Func<TEvent, CancellationToken, Task> action) =>
        _handlers.Add(typeof(TEvent), (@event, cancellationToken) => action((TEvent)@event, cancellationToken));
    public Task Handle(IEvent @event, CancellationToken ct) =>
        _handlers[@event.GetType()](@event, ct);
}

public abstract class ReadModelAction<TEntity> : Projection where TEntity : class, IRead
{
    private readonly DbSet<TEntity> _dbSet;

    protected ReadModelAction(DbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>() ?? throw new ArgumentNullException(nameof(dbContext));
    }
    protected void Projects<IEvent>(DbSet<TEntity> dbSet, Func<DbSet<TEntity>, IEvent, IQueryable<TEntity>> getEntity,
        Func<TEntity, IEvent, TEntity> handle
    )
    {
        Projects<IEvent>(async (@event, _) =>
        {
            var entity = await getEntity(dbSet, @event).FirstOrDefaultAsync(_);
            var updatedEntity = handle(entity ?? (TEntity)RuntimeHelpers.GetUninitializedObject(typeof(TEntity)),
                @event);

            if (entity == null)
            {
                await _dbSet.AddAsync(updatedEntity, _);
            }
            else
            {
                _dbSet.Update(updatedEntity);
            }
        });
    }


}