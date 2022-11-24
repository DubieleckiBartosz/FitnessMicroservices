using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record StartEnrollmentsCommand(Guid TrainingId) : ICommand<Unit>
{
    public static StartEnrollmentsCommand Create(Guid trainingId)
    {
        return new StartEnrollmentsCommand(trainingId);
    }
}