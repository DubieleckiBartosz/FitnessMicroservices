using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record CloseEnrollmentCommand(Guid EnrollmentId, Guid CloseBy) : ICommand<Unit>
{
    public static CloseEnrollmentCommand Create(Guid enrollmentId, Guid closeBy)
    {
        return new CloseEnrollmentCommand(enrollmentId, closeBy);
    }
}