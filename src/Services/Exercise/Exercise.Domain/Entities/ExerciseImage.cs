using Exercise.Domain.Base;
using Exercise.Domain.ValueObjects;

namespace Exercise.Domain.Entities;

public class ExerciseImage : Entity
{
    public Guid ExerciseId { get; private set; }
    public string ImagePath { get; private set; }
    public string ImageTitle { get; private set; }
    public bool IsMain { get; private set; }
    public ImageDescription Description { get; private set; }


    private ExerciseImage(Guid exerciseId, string path, string title, bool isMain, ImageDescription description)
    {
        if (description == null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        if (title == null)
        {
            throw new ArgumentNullException(nameof(title));
        }

        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        (this.ExerciseId, this.ImagePath, this.ImageTitle, this.IsMain, this.Description) =
            (exerciseId, path, title, isMain, description);
    }

    private ExerciseImage(Guid id, Guid exerciseId, string path, string title, bool isMain, ImageDescription description) :
        this(exerciseId, path, title, isMain, description)
    {
        this.Id = id;
    }

    public static ExerciseImage LoadImage(Guid id, Guid exerciseId, string path, string title, bool isMain,
        ImageDescription description)
    {
        return new ExerciseImage(id, exerciseId, path, title, isMain, description);
    }
    public static ExerciseImage Create(Guid exerciseId, string path, string title, bool isMain,
        ImageDescription description)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ExerciseBusinessException("Path cannot be null or empty", "The path must exist");
        }

        if (string.IsNullOrEmpty(title))
        {
            throw new ExerciseBusinessException("Title cannot be null or empty", "The title must exist");
        }

        if (title.Length <= 5 || title.Length >= 50)
        {
            throw new ExerciseBusinessException("The file title length should be between 5 and 50.", "File title bad length");
        } 

        return new ExerciseImage(exerciseId, path, title, isMain, description);
    }

    public void ChangeStatusMain()
    {
        this.IsMain = !this.IsMain;
    }
}