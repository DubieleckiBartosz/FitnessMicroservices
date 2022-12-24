using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record AssignmentEnrollmentCommand(Guid TrainingId, Guid Enrollment) : ICommand<Unit>
{
    public static AssignmentEnrollmentCommand Create(Guid trainingId, Guid enrollmentId)
    {
        return new AssignmentEnrollmentCommand(trainingId, enrollmentId);
    }
}