using Fitness.Common.Abstractions;
using MediatR;

namespace Enrollment.Application.Commands;

public record StartEnrollmentsCommand(Guid TrainingId, Guid Creator) : ICommand<Unit>
{
    public static StartEnrollmentsCommand Create(Guid trainingId, Guid creator)
    {
        return new StartEnrollmentsCommand(trainingId, creator);
    }
}