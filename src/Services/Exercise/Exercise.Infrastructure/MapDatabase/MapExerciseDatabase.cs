using Exercise.Application.Models.DataAccessObjects;

namespace Exercise.Infrastructure.MapDatabase;

public class MapExerciseDatabase
{
    public static ExerciseDetailsDao? Map(Dictionary<Guid, ExerciseDetailsDao?> dict, ExerciseDetailsDao details, ExerciseImageDao image)
    {
        if (!dict.TryGetValue(details.Id, out ExerciseDetailsDao? exercise))
        {
            exercise = details; 
            dict.Add(details.Id, exercise);
        }

        if ((exercise?.Images != null) && (!exercise.Images.Exists(_ => _.Id == image.Id)))
        {
            exercise.Images.Add(image);
        } 
        return exercise;
    }
}