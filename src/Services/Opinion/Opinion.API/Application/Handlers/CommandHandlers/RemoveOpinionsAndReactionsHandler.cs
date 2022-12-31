using Fitness.Common.Abstractions;
using MediatR;
using Opinion.API.Application.Commands;
using Opinion.API.Contracts.Repositories;
using ILogger = Serilog.ILogger;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class RemoveOpinionsAndReactionsHandler : ICommandHandler<RemoveOpinionsAndReactionsCommand, Unit>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly IReactionRepository _reactionRepository;
    private readonly ILogger _logger;

    public RemoveOpinionsAndReactionsHandler(IOpinionRepository opinionRepository, IReactionRepository reactionRepository, ILogger logger)
    {
        _opinionRepository = opinionRepository ?? throw new ArgumentNullException(nameof(opinionRepository));
        _reactionRepository = reactionRepository ?? throw new ArgumentNullException(nameof(reactionRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Unit> Handle(RemoveOpinionsAndReactionsCommand request, CancellationToken cancellationToken)
    {
        var opinionsWithReactions =
            await _opinionRepository.GetOpinionsWithReactionsByDataIdAsync(request.RemoveFrom, cancellationToken);

        _logger.Information($"Count of opinions: {opinionsWithReactions?.Count ?? 0}");

        var cnt = opinionsWithReactions?.Sum(_ => _.Reactions?.Count ?? 0);
        _logger.Information($"Count of reactions in opinions: {cnt}");

        var reactions =
            await _reactionRepository.GetReactionsWhereOpinionIsNullByDataIdAsync(request.RemoveFrom,
                cancellationToken);
         
        _logger.Information($"Count of reactions: {reactions?.Count ?? 0}");

        if (opinionsWithReactions != null && opinionsWithReactions.Any())
        {
            _opinionRepository.RemoveOpinions(opinionsWithReactions);

            _logger.Information("Start removing opinions from the database");

            await _opinionRepository.SaveAsync(cancellationToken);

            _logger.Information("------ Opinions have been removed ------");
        }

        if (reactions != null && reactions.Any())
        {
            _reactionRepository.RemoveReactions(reactions);

            _logger.Information("Start removing reactions from the database");

            await _reactionRepository.SaveAsync(cancellationToken);

            _logger.Information("------ Reactions have been removed ------");
        }

        _logger.Information("------ Removal complete ------");

        return Unit.Value;
    }
}