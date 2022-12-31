using Fitness.Common.Abstractions;
using Opinion.API.Application.Parameters;
using Opinion.API.Application.Wrappers;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Application.Commands;

public record AddReactionCommand(Guid ReactionFor, ReactionType ReactionType) : ICommand<ResponseData<long>>
{
    public static AddReactionCommand Create(AddReactionParameters parameters)
    {
        return new AddReactionCommand(parameters.ReactionFor, parameters.ReactionType);
    }
}