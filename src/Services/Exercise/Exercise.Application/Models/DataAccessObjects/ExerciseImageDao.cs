using Exercise.Domain.Entities;
using Exercise.Domain.ValueObjects;

namespace Exercise.Application.Models.DataAccessObjects;

public class ExerciseImageDao
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; private set; }
    public string ImagePath { get; init; }
    public string ImageTitle { get; init; }
    public bool IsMain { get; init; }
    public string? Description { get; init; }

    public ExerciseImageDao()
    {
    }

    public ExerciseImageDao(Guid id, string imagePath, string imageTitle, bool isMain, string? description)
    {
        Id = id;
        ImagePath = imagePath;
        ImageTitle = imageTitle;
        IsMain = isMain;
        Description = description;
    }

    public ExerciseImage Map()
    {
        return ExerciseImage.LoadImage(Id, ExerciseId, ImagePath, ImageTitle, IsMain,
            Description != null ? ImageDescription.Create(Description) : null);
    }
}