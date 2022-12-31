using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Opinion.API.Application.Commands;
using Opinion.API.Application.Wrappers;
using Opinion.API.Constants;
using Opinion.API.Contracts.Repositories;
using Opinion.API.Domain; 
using ILogger = Serilog.ILogger;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class AddReactionHandler : ICommandHandler<AddReactionCommand, ResponseData<long>>
{
    private readonly IReactionRepository _reactionRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger _logger;

    public AddReactionHandler(IReactionRepository reactionRepository, ICurrentUser currentUser, ILogger logger)
    {
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<ResponseData<long>> Handle(AddReactionCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUser.UserName;
        var newReaction = Reaction.Create(request.ReactionFor, user, request.ReactionType);


        _logger.Information("-------- Creating new reaction in database --------");

        await _reactionRepository.AddReactionAsync(newReaction, cancellationToken);
        await _reactionRepository.SaveAsync(cancellationToken);

        _logger.Information($"New opinion has been approved: {newReaction.Id}"); 

        return ResponseData<long>.Ok(newReaction.Id, StringMessages.ReactionAddedMessage);
    }
}