using Fitness.Common.Abstractions;
using Opinion.API.Application.Parameters;
using Opinion.API.Application.Wrappers;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Application.Commands;

public record AddReactionToOpinionCommand(long OpinionId, ReactionType ReactionType) : ICommand<ResponseData<long>>
{
    public static AddReactionToOpinionCommand Create(AddReactionToOpinionParameters parameters)
    {
        return new AddReactionToOpinionCommand(parameters.OpinionId, parameters.ReactionType);
    }
}