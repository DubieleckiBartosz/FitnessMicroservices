using Exercise.Application.Contracts;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.CreateNewExercise;

public class CreateNewExerciseHandler : ICommandHandler<CreateNewExerciseCommand, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;

    public CreateNewExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public Task<Guid> Handle(CreateNewExerciseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}