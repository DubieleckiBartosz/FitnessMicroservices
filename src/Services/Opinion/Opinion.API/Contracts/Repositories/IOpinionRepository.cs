namespace Opinion.API.Contracts.Repositories;

public interface IOpinionRepository
{
    Task<List<Domain.Opinion>?> GetOpinionsWithReactionsByDataIdAsync(Guid dataId,
        CancellationToken cancellationToken = default);
    Task<Domain.Opinion?> GetOpinionWithReactionsAsync(long opinionId, CancellationToken cancellationToken = default);
    Task AddOpinionAsync(Domain.Opinion opinion, CancellationToken cancellationToken = default);
    void RemoveOpinion(Domain.Opinion opinion);
    void RemoveOpinions(List<Domain.Opinion> opinions);
    void UpdateOpinion(Domain.Opinion opinion);
    Task SaveAsync(CancellationToken cancellationToken = default);
}