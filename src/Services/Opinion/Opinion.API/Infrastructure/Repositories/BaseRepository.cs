using Microsoft.EntityFrameworkCore;
using Opinion.API.Infrastructure.Database; 

namespace Opinion.API.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    private readonly OpinionContext _dbContext;
    protected DbSet<TEntity> DbSet { get; }
    protected BaseRepository(OpinionContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.DbSet = dbContext.Set<TEntity>();
    }

    protected async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken); 
    }
    protected void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    protected void RemoveRange(List<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    } 

    protected void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
    protected async Task SaveDataAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}