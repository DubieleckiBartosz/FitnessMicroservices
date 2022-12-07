using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record NewTrainingEnrollmentCommand(Guid UserId, Guid EnrollmentId) : ICommand<Unit>
{
    public static NewTrainingEnrollmentCommand Create(Guid userId, Guid enrollmentId)
    {
        return new NewTrainingEnrollmentCommand(userId, enrollmentId);
    }
}