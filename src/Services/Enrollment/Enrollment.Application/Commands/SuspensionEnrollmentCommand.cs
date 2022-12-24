using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record SuspensionEnrollmentCommand(Guid EnrollmentId, Guid SuspensionBy) : ICommand<Unit>
{
    public static SuspensionEnrollmentCommand Create(Guid enrollmentId, Guid suspensionBy)
    {
        return new SuspensionEnrollmentCommand(enrollmentId, suspensionBy);
    }
}