using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using MediatR;
using Opinion.API.Application.Commands;
using Opinion.API.Constants;
using Opinion.API.Contracts.Repositories;
using ILogger = Serilog.ILogger;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class RemoveReactionHandler : ICommandHandler<RemoveReactionCommand, Unit>
{
    private readonly IReactionRepository _reactionRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger _logger;

    public RemoveReactionHandler(IReactionRepository reactionRepository, ICurrentUser currentUser, ILogger logger)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Unit> Handle(RemoveReactionCommand request, CancellationToken cancellationToken)
    {
        var reaction = await _reactionRepository.GetReactionByIdAsync(request.ReactionId, cancellationToken);
        if (reaction == null)
        {
            throw new NotFoundException(StringMessages.ReactionNotFoundMessage, StringMessages.ReactionNotFoundTitle);
        }

        var currentUser = _currentUser.UserName;
        if (reaction.User != currentUser)
        {
            throw new NotFoundException(StringMessages.NoPermissionsToDeleteReactionMessage, StringMessages.NoPermissionsTitle);
        }

        _logger.Information("Start removing reaction from the database");

        _reactionRepository.RemoveReaction(reaction);

        _logger.Information($"------ Reaction has been removed: {reaction.Id}------");

        return Unit.Value;
    }
}