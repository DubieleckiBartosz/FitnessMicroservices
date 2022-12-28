using Exercise.Application.Contracts;
using Exercise.Domain.ValueObjects;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseDescription;

public class UpdateExerciseDescriptionHandler : ICommandHandler<UpdateExerciseDescriptionCommand, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseDescriptionHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public async Task<Unit> Handle(UpdateExerciseDescriptionCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exercise == null)
        {
            throw new NotFoundException("Exercise cannot be null.", "Exercise not found");
        }

        exercise.UpdateDescription(ExerciseDescription.Create(request.Description));
        await _exerciseRepository.UpdateAsync(exercise);

        return Unit.Value;
    }
}