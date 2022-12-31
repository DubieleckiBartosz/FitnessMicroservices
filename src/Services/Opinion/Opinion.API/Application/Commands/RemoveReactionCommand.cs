using Fitness.Common.Abstractions;
using MediatR;
using Opinion.API.Application.Parameters;

namespace Opinion.API.Application.Commands;

public record RemoveReactionCommand(long ReactionId) : ICommand<Unit>
{
    public static RemoveReactionCommand Create(RemoveReactionParameters parameters)
    {
        return new RemoveReactionCommand(parameters.ReactionId);
    }
}