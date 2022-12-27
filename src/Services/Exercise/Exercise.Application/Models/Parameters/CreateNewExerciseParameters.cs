using Newtonsoft.Json;

namespace Exercise.Application.Models.Parameters;

public class CreateNewExerciseParameters
{
    public string Name { get; init; }
    public string Description { get; init; }

    [JsonConstructor]
    public CreateNewExerciseParameters(string name, string description)
    {
        Name = name;
        Description = description;
    }
}