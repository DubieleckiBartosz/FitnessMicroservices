using Fitness.Common.Abstractions;
using MediatR;

namespace Opinion.API.Application.Commands;

public record RemoveOpinionsAndReactionsCommand(Guid RemoveFrom, string User) : ICommand<Unit>
{
    public static RemoveOpinionsAndReactionsCommand Create(Guid removeFrom, string user)
    {
        return new RemoveOpinionsAndReactionsCommand(removeFrom, user);
    }
}