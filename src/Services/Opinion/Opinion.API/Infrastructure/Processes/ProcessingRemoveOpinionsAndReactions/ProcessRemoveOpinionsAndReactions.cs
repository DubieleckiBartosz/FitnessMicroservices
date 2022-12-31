using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Events;
using Opinion.API.Application.Commands;

namespace Opinion.API.Infrastructure.Processes.ProcessingRemoveOpinionsAndReactions;

public class ProcessRemoveOpinionsAndReactions : IEventHandler<ExternalDataRemoved>
{
    private readonly ICommandBus _commandBus;

    public ProcessRemoveOpinionsAndReactions(ICommandBus commandBus)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }
    public async Task Handle(ExternalDataRemoved notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var command = RemoveOpinionsAndReactionsCommand.Create(removeFrom: notification.TrainingId);
        await _commandBus.Send(command, cancellationToken);
    }
}