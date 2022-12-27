using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseDescription;

public record UpdateExerciseDescriptionCommand(Guid ExerciseId, string Description) : ICommand<Unit>
{
    public static UpdateExerciseDescriptionCommand Create(UpdateExerciseDescriptionParameters parameters)
    {
        return new UpdateExerciseDescriptionCommand(parameters.ExerciseId, parameters.Description);
    }
}