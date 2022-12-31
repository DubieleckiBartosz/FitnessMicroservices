using Microsoft.EntityFrameworkCore;
using Opinion.API.Contracts.Repositories;
using Opinion.API.Domain;
using Opinion.API.Infrastructure.Database;

namespace Opinion.API.Infrastructure.Repositories;

public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
{
    public ReactionRepository(OpinionContext dbContext) : base(dbContext)
    {
    }
    public async Task<List<Reaction>?> GetReactionsWhereOpinionIsNullByDataIdAsync(Guid dataId, CancellationToken cancellationToken = default)
    {
        return await this.DbSet.Where(_ => _.ReactionFor == dataId && _.OpinionId == null).ToListAsync(cancellationToken);
    }

    public async Task AddReactionAsync(Reaction reaction, CancellationToken cancellationToken = default)
    {
        await AddAsync(reaction, cancellationToken);
    }

    public void RemoveReaction(Reaction reaction)
    {
        Remove(reaction);
    }

    public void RemoveReactions(List<Reaction> reactions)
    {
        this.RemoveRange(reactions);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveDataAsync(cancellationToken);
    }
}