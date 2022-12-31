using Opinion.API.Domain;

namespace Opinion.API.Contracts.Repositories;

public interface IReactionRepository
{
    Task<List<Reaction>?> GetReactionsWhereOpinionIsNullByDataIdAsync(Guid dataId, CancellationToken cancellationToken = default);
    Task AddReactionAsync(Reaction reaction, CancellationToken cancellationToken = default);
    void RemoveReaction(Reaction reaction);
    void RemoveReactions(List<Reaction> reactions);
    Task SaveAsync(CancellationToken cancellationToken = default);
}