using Exercise.Application.Contracts;
using Fitness.Common.Abstractions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseVideoLink;

public class UpdateExerciseVideoLinkHandler : ICommandHandler<UpdateExerciseVideoLinkCommand, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseVideoLinkHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public Task<Unit> Handle(UpdateExerciseVideoLinkCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}