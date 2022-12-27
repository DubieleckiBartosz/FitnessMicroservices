using Newtonsoft.Json;

namespace Exercise.Application.Models.Parameters;

public class AddNewImageToExerciseParameters
{
    public Guid ExerciseId { get; init; }
    public string ImagePath { get; init; }
    public string ImageTitle { get; init; }
    public bool IsMain { get; init; }
    public string Description { get; init; }

    public AddNewImageToExerciseParameters()
    {
    }

    [JsonConstructor]
    public AddNewImageToExerciseParameters(Guid exerciseId, string imagePath, string imageTitle, bool isMain, string description)
    {
        ExerciseId = exerciseId;
        ImagePath = imagePath;
        ImageTitle = imageTitle;
        IsMain = isMain;
        Description = description;
    }
}