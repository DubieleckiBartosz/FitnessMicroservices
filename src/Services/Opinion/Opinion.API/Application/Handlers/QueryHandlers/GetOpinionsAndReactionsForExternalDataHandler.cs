using Fitness.Common.Abstractions;
using Opinion.API.Application.Queries;
using Opinion.API.Application.Views;
using Opinion.API.Application.Wrappers;
using Opinion.API.Contracts.Repositories;

namespace Opinion.API.Application.Handlers.QueryHandlers;

public class GetOpinionsAndReactionsForExternalDataHandler : IQueryHandler<GetOpinionsForExternalEntityQuery,
    ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly IReactionRepository _reactionRepository;

    public GetOpinionsAndReactionsForExternalDataHandler(IOpinionRepository opinionRepository, IReactionRepository reactionRepository)
    {
        _opinionRepository = opinionRepository ?? throw new ArgumentNullException(nameof(opinionRepository));
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
    }

    public async Task<ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>> Handle(
        GetOpinionsForExternalEntityQuery request, CancellationToken cancellationToken)
    {
        var opinionsWithReactions =
            await _opinionRepository.GetOpinionsWithReactionsByDataIdAsync(request.DataId, cancellationToken);  

        var reactions =
            await _reactionRepository.GetReactionsWhereOpinionIsNullByDataIdAsync(request.DataId,
                cancellationToken);


        var opinionViews = opinionsWithReactions?.Select(OpinionViewModel.Map).ToList();
        var reactionViews = reactions?.Select(ReactionViewModel.Map).ToList();

        var data = GetOpinionsAndReactionsForExternalDataViewModel.Create(opinionViews, reactionViews);

        return ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>.Ok(data, null);
    }
}