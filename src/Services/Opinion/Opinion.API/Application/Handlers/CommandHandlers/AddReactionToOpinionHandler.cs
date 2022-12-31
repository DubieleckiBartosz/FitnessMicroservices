using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Opinion.API.Application.Commands;
using Opinion.API.Application.Wrappers;
using Opinion.API.Constants;
using Opinion.API.Contracts.Repositories;
using ILogger = Serilog.ILogger;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class AddReactionToOpinionHandler : ICommandHandler<AddReactionToOpinionCommand, ResponseData<long>>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger _logger;

    public AddReactionToOpinionHandler(IOpinionRepository opinionRepository, ICurrentUser currentUser, ILogger logger)
    {
        _opinionRepository = opinionRepository ?? throw new ArgumentNullException(nameof(opinionRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<ResponseData<long>> Handle(AddReactionToOpinionCommand request, CancellationToken cancellationToken)
    {
        var opinion = await _opinionRepository.GetOpinionWithReactionsAsync(request.OpinionId, cancellationToken);
        if (opinion == null)
        {
            throw new NotFoundException(StringMessages.OpinionNotFoundMessage, StringMessages.OpinionNotFoundTitle);
        }

        var user = _currentUser.UserName;
        var reaction = opinion.NewReaction(user, request.ReactionType);

        _logger.Information($"-------- Update of opinion {opinion.Id} in the database --------");

        _opinionRepository.UpdateOpinion(opinion);
        await _opinionRepository.SaveAsync(cancellationToken);

        _logger.Information($"New reaction has been added: {reaction.Id}");

        return ResponseData<long>.Ok(reaction.Id, StringMessages.ReactionAddedMessage);
    }
}