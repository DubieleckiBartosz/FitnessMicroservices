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

    public GetOpinionsAndReactionsForExternalDataHandler(IOpinionRepository opinionRepository)
    {
        _opinionRepository = opinionRepository ?? throw new ArgumentNullException(nameof(opinionRepository));
    }

    public Task<ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>> Handle(
        GetOpinionsForExternalEntityQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}