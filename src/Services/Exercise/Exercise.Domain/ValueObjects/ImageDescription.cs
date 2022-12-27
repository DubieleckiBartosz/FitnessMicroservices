using Exercise.Domain.Base;

namespace Exercise.Domain.ValueObjects;

public class ImageDescription : ValueObject
{ 
    public string Description { get; private set; }

    private ImageDescription(string imageDescription)
    {
        this.Description = imageDescription;
    }

    public static ImageDescription Create(string imageDescription)
    {
        if (string.IsNullOrEmpty(imageDescription))
        {
            throw new ExerciseBusinessException("Image description cannot be null or empty", "The description must exist");
        }

        if (imageDescription.Length <= 10)
        {
            throw new ExerciseBusinessException("Description length should be longer than 10 characters.", "Description bad length"); 
        }

        return new ImageDescription(imageDescription);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Description;
    }
} 