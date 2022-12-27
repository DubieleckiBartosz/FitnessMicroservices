using Newtonsoft.Json;

namespace Exercise.Application.Models.Parameters;

public class UpdateExerciseVideoLinkParameters
{
    public Guid ExerciseId { get; init; }
    public string Link { get; init; }

    [JsonConstructor]
    public UpdateExerciseVideoLinkParameters(Guid exerciseId, string link)
    {
        ExerciseId = exerciseId;
        Link = link;
    }
}