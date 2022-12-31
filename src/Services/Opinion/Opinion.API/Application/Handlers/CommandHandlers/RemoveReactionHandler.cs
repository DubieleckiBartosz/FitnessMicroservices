using Fitness.Common.Abstractions;
using MediatR;
using Opinion.API.Application.Commands;
using Opinion.API.Contracts.Repositories;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class RemoveReactionHandler : ICommandHandler<RemoveReactionCommand, Unit>
{
    private readonly IReactionRepository _reactionRepository;

    public RemoveReactionHandler(IReactionRepository reactionRepository)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
    }
    public Task<Unit> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}