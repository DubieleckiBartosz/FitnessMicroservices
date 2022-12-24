namespace Training.API.Processes.ProcessingAssignmentEnrollment;

public class ProcessAssignmentEnrollment : IEventHandler<EnrollmentAssigned>
{
    private readonly ICommandBus commandBus;

    public ProcessAssignmentEnrollment(ICommandBus commandBus)
    {
        if (commandBus is null)
        {
            throw new ArgumentNullException(nameof(commandBus));
        }

        this.commandBus = commandBus;
    }
    public async Task Handle(EnrollmentAssigned notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        var command = AssignmentEnrollmentCommand.Create(notification.TrainingId, notification.EnrollmentId);
        await commandBus.Send(command);
    }
}