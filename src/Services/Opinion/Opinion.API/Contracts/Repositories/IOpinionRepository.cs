namespace Opinion.API.Contracts.Repositories;

public interface IOpinionRepository
{
    Task AddOpinionAsync(Domain.Opinion opinion, CancellationToken cancellationToken = default);
    void DeleteOpinion(Domain.Opinion opinion);
    void UpdateOpinion(Domain.Opinion opinion);
}