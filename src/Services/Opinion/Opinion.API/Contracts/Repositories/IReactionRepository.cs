using Opinion.API.Domain;

namespace Opinion.API.Contracts.Repositories;

public interface IReactionRepository
{
    Task AddReactionAsync(Reaction reaction, CancellationToken cancellationToken = default);
    void RemoveReaction(Reaction reaction);
}