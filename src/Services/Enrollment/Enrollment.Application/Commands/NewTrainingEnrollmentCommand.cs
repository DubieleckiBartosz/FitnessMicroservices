using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record NewTrainingEnrollmentCommand(Guid EnrollmentId) : ICommand<Unit>
{
    public static NewTrainingEnrollmentCommand Create(Guid enrollmentId)
    {
        return new NewTrainingEnrollmentCommand(enrollmentId);
    }
}