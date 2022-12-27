using Exercise.Application.Contracts;
using Fitness.Common.Abstractions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseDescription;

public class UpdateExerciseDescriptionHandler : ICommandHandler<UpdateExerciseDescriptionCommand, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseDescriptionHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public Task<Unit> Handle(UpdateExerciseDescriptionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}