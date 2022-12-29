using Opinion.API.Contracts.Repositories;
using Opinion.API.Infrastructure.Database;

namespace Opinion.API.Infrastructure.Repositories;

public class OpinionRepository : BaseRepository<Domain.Opinion>, IOpinionRepository
{
    public OpinionRepository(OpinionContext dbContext) : base(dbContext)
    {
    }
    public async Task AddOpinionAsync(Domain.Opinion opinion, CancellationToken cancellationToken = default)
    {
        await this.AddAsync(opinion, cancellationToken);
    }

    public void DeleteOpinion(Domain.Opinion opinion)
    {
        this.Remove(opinion);
    }

    public void UpdateOpinion(Domain.Opinion opinion)
    {
         this.UpdateOpinion(opinion);
    } 
}