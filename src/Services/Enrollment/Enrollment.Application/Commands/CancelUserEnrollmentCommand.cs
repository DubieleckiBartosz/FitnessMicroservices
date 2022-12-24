using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record CancelUserEnrollmentCommand(Guid EnrollmentId, Guid UserEnrollment) : ICommand<Unit>
{
    public static CancelUserEnrollmentCommand Create(Guid enrollmentId, Guid userEnrollment)
    {
        return new CancelUserEnrollmentCommand(enrollmentId, userEnrollment);
    }
}