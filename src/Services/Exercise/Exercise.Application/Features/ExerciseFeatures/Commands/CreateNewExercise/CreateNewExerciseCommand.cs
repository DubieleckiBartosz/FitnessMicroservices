using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.CreateNewExercise;

public record CreateNewExerciseCommand(string Name, string Description) : ICommand<Guid>
{
    public static CreateNewExerciseCommand Create(CreateNewExerciseParameters parameters)
    {
        return new CreateNewExerciseCommand(parameters.Name, parameters.Description);
    }
}