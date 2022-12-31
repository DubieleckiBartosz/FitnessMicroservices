using Fitness.Common.Abstractions;
using MediatR;

namespace Opinion.API.Application.Commands;

public record RemoveOpinionsAndReactionsCommand(Guid RemoveFrom) : ICommand<Unit>
{
    public static RemoveOpinionsAndReactionsCommand Create(Guid removeFrom)
    {
        return new RemoveOpinionsAndReactionsCommand(removeFrom);
    }
}