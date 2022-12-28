using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.AddNewImageToExercise;

public record AddNewImageToExerciseCommand(IFormFile Image, Guid ExerciseId, string Path, string Title, bool IsMain,
    string Description) : ICommand<Guid>
{
    public static AddNewImageToExerciseCommand Create(AddNewImageToExerciseParameters parameters)
    {
        return new AddNewImageToExerciseCommand(parameters.Image, parameters.ExerciseId, parameters.ImagePath,
            parameters.ImageTitle, parameters.IsMain, parameters.Description);
    }
}