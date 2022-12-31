using Fitness.Common.Abstractions;
using Opinion.API.Application.Parameters;
using Opinion.API.Application.Wrappers;

namespace Opinion.API.Application.Commands;

public record AddOpinionCommand(Guid OpinionFor, string Comment) : ICommand<ResponseData<long>>
{
    public static AddOpinionCommand Create(AddOpinionParameters parameters)
    {
        return new AddOpinionCommand(parameters.OpinionFor, parameters.Comment);
    }
}