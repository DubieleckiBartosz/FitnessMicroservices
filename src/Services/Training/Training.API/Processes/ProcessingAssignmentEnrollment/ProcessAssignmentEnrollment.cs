namespace Training.API.Processes.ProcessingAssignmentEnrollment;

public class ProcessAssignmentEnrollment : IEventHandler<EnrollmentAssigned>
{
    private readonly ICommandBus _commandBus;

    public ProcessAssignmentEnrollment(ICommandBus commandBus)
    {
        this._commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }
    public async Task Handle(EnrollmentAssigned notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var command = AssignmentEnrollmentCommand.Create(notification.TrainingId, notification.EnrollmentId);
        await _commandBus.Send(command, cancellationToken);
    }
}