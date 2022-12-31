using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Logging;
using Opinion.API.Application.Commands;
using Opinion.API.Application.Wrappers;
using Opinion.API.Constants;
using Opinion.API.Contracts.Repositories;

namespace Opinion.API.Application.Handlers.CommandHandlers;

public class AddOpinionHandler : ICommandHandler<AddOpinionCommand, ResponseData<long>>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILoggerManager<AddOpinionHandler> _loggerManager;

    public AddOpinionHandler(IOpinionRepository opinionRepository, ICurrentUser currentUser, ILoggerManager<AddOpinionHandler> loggerManager)
    {
        _opinionRepository = opinionRepository ?? throw new ArgumentNullException(nameof(opinionRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
    }
    public async Task<ResponseData<long>> Handle(AddOpinionCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUser.UserName;
        var opinion = Domain.Opinion.Create(request.OpinionFor, request.Comment, user);

        _loggerManager.LogInformation(null, "-------- Creating new opinion in database --------");

        await _opinionRepository.AddOpinionAsync(opinion, cancellationToken);
        await _opinionRepository.SaveAsync(cancellationToken);

        _loggerManager.LogInformation(new
        {
            NewOpinion = opinion.Id,
            Message = "New opinion has been approved"
        });

        return ResponseData<long>.Ok(opinion.Id, StringMessages.NewOpinionCreatedMessage);
    }
}