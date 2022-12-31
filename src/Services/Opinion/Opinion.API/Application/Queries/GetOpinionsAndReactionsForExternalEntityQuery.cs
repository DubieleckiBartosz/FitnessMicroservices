using Fitness.Common.Abstractions;
using Opinion.API.Application.Views;
using Opinion.API.Application.Wrappers;

namespace Opinion.API.Application.Queries;

public record GetOpinionsForExternalEntityQuery(Guid DataId) : IQuery<ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>>
{
    public static GetOpinionsForExternalEntityQuery Create(Guid dataId)
    {
        return new GetOpinionsForExternalEntityQuery(DataId: dataId);
    }
}