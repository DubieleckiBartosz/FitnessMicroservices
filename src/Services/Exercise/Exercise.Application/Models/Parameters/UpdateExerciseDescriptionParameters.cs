using Newtonsoft.Json;

namespace Exercise.Application.Models.Parameters;

public class UpdateExerciseDescriptionParameters
{
    public Guid ExerciseId { get; init; }
    public string Description { get; init; }

    [JsonConstructor]
    public UpdateExerciseDescriptionParameters(Guid exerciseId, string description)
    {
        ExerciseId = exerciseId;
        Description = description;
    }
}