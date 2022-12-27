using Exercise.Domain.Base;

namespace Exercise.Domain.ValueObjects;

public class Link : ValueObject
{
    public string VideoLink { get; private set; }

    private Link(string videoLink)
    {
        VideoLink = videoLink;
    }

    public static Link Create(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            throw new ExerciseBusinessException("Link cannot be null or empty", "The link must exist");
        }

        return new Link(link);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return VideoLink;
    }
}