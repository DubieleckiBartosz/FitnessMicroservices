using Fitness.Common.Extensions;
using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public class TrainingDetails : IRead 
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public Guid TrainerUniqueCode { get; set; }
    public bool IsActive { get; set; }
    public decimal? Price { get; set; }
    public TrainingStatus Status { get; set; }
    public TrainingAvailability Availability { get; set; }
    public TrainingType? Type { get; set; }
    public int? DurationTrainingInMinutes { get; set; }
    public int? BreakBetweenExercisesInMinutes { get; set; }
    public DateTime Created { get; set; }
    public List<TrainingExercise> TrainingExercises { get; set; }
    public List<TrainingUser> TrainingUsers { get; set; }

    internal TrainingDetails()
    {
    }
    private TrainingDetails(Guid trainingId, Guid trainerUniqueCode, DateTime created)
    {
        Id = trainingId;
        TrainerUniqueCode = trainerUniqueCode;
        IsActive = false;
        Status = TrainingStatus.Init;
        Availability = TrainingAvailability.Private;
        Created = created;
    }

    public static TrainingDetails Create(NewTrainingInitiated @event)
    {
        return new TrainingDetails(@event.TrainingId, @event.TrainerUniqueCode, @event.Created);
    } 

    public TrainingDetails UserAdded(UserToTrainingAdded @event)
    {
        var user = TrainingUsers.First(_ => _.Id == @event.UserId);
        TrainingUsers.Remove(user);

        return this;
    }

    public TrainingDetails NewExerciseAdded(ExerciseAdded @event)
    {
        if (!TrainingExercises.Any())
        {
            Status = TrainingStatus.InProgress;
        }

        var newExercise = @event.Exercise;
        var exercise = TrainingExercises.FirstOrDefault(_ => _.ExternalExerciseId == newExercise.ExternalExerciseId);
        if (exercise == null)
        {
            TrainingExercises.Add(newExercise);
        }
        else
        {
            TrainingExercises.Replace(exercise,
                exercise.Update(newExercise.NumberRepetitions, newExercise.BreakBetweenSetsInMinutes));
        }

        return this;
    }

    public TrainingDetails TrainingExerciseRemoved(ExerciseRemoved @event)
    {
        var exercise = TrainingExercises.First(_ => _.Id == @event.ExerciseId);
        TrainingExercises.Remove(exercise);

        return this;
    }

}