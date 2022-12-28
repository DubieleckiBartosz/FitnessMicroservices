using Exercise.Domain.ValueObjects;

namespace Exercise.Application.Models.DataAccessObjects;

public class ExerciseDao
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string CreatedBy { get; init; }
    public string? Video { get; init; }
    public string Description { get; init; }

    public ExerciseDao()
    {
    }
    public ExerciseDao(Guid id, string name, string createdBy, string? video, string description)
    {
        Id = id;
        Name = name;
        CreatedBy = createdBy;
        Video = video;
        Description = description;
    }

    public Domain.Entities.Exercise Map()
    { 
        return Domain.Entities.Exercise.Load(this.Id, this.Name, this.CreatedBy, ExerciseDescription.Create(this.Description));
    }
}