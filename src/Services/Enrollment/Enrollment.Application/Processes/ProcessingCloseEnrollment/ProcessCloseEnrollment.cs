using Enrollment.Application.Commands;
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingCloseEnrollment;

public class ProcessCloseEnrollment : IEventHandler<TrainingClosed>
{
    private readonly ICommandBus _commandBus;

    public ProcessCloseEnrollment(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    public async Task Handle(TrainingClosed notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var command = CloseEnrollmentCommand.Create(notification.EnrollmentId, notification.MarkedBy);
        await _commandBus.Send(command, cancellationToken);
    }
}