using Opinion.API.Contracts.Repositories;
using Opinion.API.Domain;
using Opinion.API.Infrastructure.Database;

namespace Opinion.API.Infrastructure.Repositories;

public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
{
    public ReactionRepository(OpinionContext dbContext) : base(dbContext)
    {
    }

    public async Task AddReactionAsync(Reaction reaction, CancellationToken cancellationToken = default)
    {
        await AddAsync(reaction, cancellationToken);
    }

    public void RemoveReaction(Reaction reaction)
    {
        Remove(reaction);
    }
}