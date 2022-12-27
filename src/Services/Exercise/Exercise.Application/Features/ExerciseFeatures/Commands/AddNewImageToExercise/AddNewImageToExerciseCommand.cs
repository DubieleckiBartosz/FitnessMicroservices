using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.AddNewImageToExercise;

public record AddNewImageToExerciseCommand(Guid ExerciseId, string Path, string Title, bool IsMain, string Description) : ICommand<Guid>
{
    public static AddNewImageToExerciseCommand Create(AddNewImageToExerciseParameters parameters)
    {
        return new AddNewImageToExerciseCommand(parameters.ExerciseId, parameters.ImagePath, parameters.ImageTitle, parameters.IsMain, parameters.Description);
    }
}