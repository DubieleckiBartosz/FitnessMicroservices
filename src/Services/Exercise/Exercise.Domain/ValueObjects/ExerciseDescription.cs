using Exercise.Domain.Base;

namespace Exercise.Domain.ValueObjects;

public class ExerciseDescription : ValueObject
{
    public string Description { get; private init; }

    private ExerciseDescription(string eventDescription)
    {
        this.Description = eventDescription;
    }

    public static ExerciseDescription Create(string eventDescription)
    {
        if (string.IsNullOrEmpty(eventDescription))
        {
            throw new ExerciseBusinessException("Exercise description cannot be null or empty", "The description must exist");
        } 

        return new ExerciseDescription(eventDescription);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Description;
    }
}