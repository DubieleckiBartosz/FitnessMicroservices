using Exercise.Domain.ValueObjects;

namespace Exercise.Application.Models.DataAccessObjects;

public class ExerciseDetailsDao : ExerciseDao
{ 
    public List<ExerciseImageDao> Images { get; init; } = new List<ExerciseImageDao>();

    public Domain.Entities.Exercise Map()
    {
        var images = this.Images.Select(_ => _.Map()).ToList();
        return Domain.Entities.Exercise.Load(this.Id, this.Name, this.CreatedBy, ExerciseDescription.Create(this.Description), images);
    }
}