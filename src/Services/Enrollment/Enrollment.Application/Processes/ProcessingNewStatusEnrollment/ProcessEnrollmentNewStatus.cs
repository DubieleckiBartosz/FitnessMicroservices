using Enrollment.Application.Commands;
using Enrollment.Application.Processes.TrainingEnums;
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingNewStatusEnrollment;

public class ProcessEnrollmentNewStatus : IEventHandler<TrainingAvailabilityChanged>
{
    private readonly ICommandBus _commandBus;

    public ProcessEnrollmentNewStatus(ICommandBus commandBus)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }

    public async Task Handle(TrainingAvailabilityChanged notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        if (notification.NewAvailability == TrainingAvailability.Group)
        {
            var command = OpenEnrollmentCommand.Create(notification.EnrollmentId, notification.ChangedBy);
            await _commandBus.Send(command, cancellationToken);
        }
        else
        {
            var command = SuspensionEnrollmentCommand.Create(notification.EnrollmentId, notification.ChangedBy);
            await _commandBus.Send(command, cancellationToken);
        }
    }
}