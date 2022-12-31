using Enrollment.Application.Commands;
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Events; 

namespace Enrollment.Application.Processes.ProcessingStartEnrollments;

public class ProcessStartEnrollments : IEventHandler<TrainingPublished>
{
    private readonly ICommandBus _commandBus; 

    public ProcessStartEnrollments(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    public async Task Handle(TrainingPublished notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var command = StartEnrollmentsCommand.Create(notification.TrainingId, notification.TrainerCode);
        await _commandBus.Send(command, cancellationToken);
    }
}