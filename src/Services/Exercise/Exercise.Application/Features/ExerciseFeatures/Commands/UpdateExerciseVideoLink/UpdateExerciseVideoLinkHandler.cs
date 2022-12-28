using Exercise.Application.Contracts;
using Exercise.Domain.ValueObjects;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseVideoLink;

public class UpdateExerciseVideoLinkHandler : ICommandHandler<UpdateExerciseVideoLinkCommand, Unit>
{
    private readonly IExerciseRepository _exerciseRepository;

    public UpdateExerciseVideoLinkHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public async Task<Unit> Handle(UpdateExerciseVideoLinkCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exercise == null)
        {
            throw new NotFoundException("Exercise cannot be null.", "Exercise not found");
        }

        exercise.AssignVideoLink(Link.Create(request.Link));
        await _exerciseRepository.UpdateAsync(exercise);

        return Unit.Value;
    }
}