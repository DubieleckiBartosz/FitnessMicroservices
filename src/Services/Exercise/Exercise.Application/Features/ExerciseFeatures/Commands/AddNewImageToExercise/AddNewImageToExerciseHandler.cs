using Exercise.Application.Contracts;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.AddNewImageToExercise;

public class AddNewImageToExerciseHandler : ICommandHandler<AddNewImageToExerciseCommand, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;

    public AddNewImageToExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }
    public Task<Guid> Handle(AddNewImageToExerciseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}