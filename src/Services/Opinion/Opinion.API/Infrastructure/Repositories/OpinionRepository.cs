using Microsoft.EntityFrameworkCore;
using Opinion.API.Contracts.Repositories;
using Opinion.API.Infrastructure.Database;

namespace Opinion.API.Infrastructure.Repositories;

public class OpinionRepository : BaseRepository<Domain.Opinion>, IOpinionRepository
{
    public OpinionRepository(OpinionContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Opinion?> GetOpinionWithReactionsAsync(long opinionId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Include(_ => _.Reactions).FirstOrDefaultAsync(_ => _.Id == opinionId, cancellationToken);
    }

    public async Task<List<Domain.Opinion>?> GetOpinionsWithReactionsByDataIdAsync(Guid dataId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.Include(_ => _.Reactions).Where(_ => _.OpinionFor == dataId).ToListAsync(cancellationToken);
    }

    public async Task AddOpinionAsync(Domain.Opinion opinion, CancellationToken cancellationToken = default)
    {
        await AddAsync(opinion, cancellationToken);
    }

    public void RemoveOpinion(Domain.Opinion opinion)
    {
        Remove(opinion);
    }

    public void RemoveOpinions(List<Domain.Opinion> opinions)
    {
        this.RemoveRange(opinions);
    }

    public void UpdateOpinion(Domain.Opinion opinion)
    {
         this.Update(opinion);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await this.SaveDataAsync(cancellationToken);
    }
}