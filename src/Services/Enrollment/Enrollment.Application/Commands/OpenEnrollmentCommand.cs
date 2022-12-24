using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record OpenEnrollmentCommand(Guid EnrollmentId, Guid OpenBy) : ICommand<Unit>
{
    public static OpenEnrollmentCommand Create(Guid enrollmentId, Guid openBy)
    {
        return new OpenEnrollmentCommand(enrollmentId, openBy);
    }
}