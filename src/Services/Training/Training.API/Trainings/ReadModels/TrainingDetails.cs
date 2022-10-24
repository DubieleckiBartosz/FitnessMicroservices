using Fitness.Common.Projection;
using Training.API.Trainings.Enums;
using Training.API.Trainings.TrainingEvents;

namespace Training.API.Trainings.ReadModels;

public class TrainingDetails : IRead
{
    public Guid TrainingId { get; set; }
    public Guid CreatorId { get; set; }
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

    public TrainingDetails()
    {
    }
    private TrainingDetails(Guid trainingId, Guid creatorId, DateTime created)
    {
        TrainingId = trainingId;
        CreatorId = creatorId;
        IsActive = false;
        Status = TrainingStatus.Created;
        Availability = TrainingAvailability.Private;
        Created = created;
    }

    public static TrainingDetails Create(NewTrainingInitiated @event)
    {
        return new TrainingDetails(@event.TrainingId, @event.TrainerId, @event.Created);
    } 

    public TrainingDetails UserAdded(UserToTrainingAdded @event)
    {
        var user = TrainingUsers.First(_ => _.Id == @event.UserId);
        TrainingUsers.Remove(user);

        return this;
    }

    public TrainingDetails NewExerciseAdded(ExerciseAdded @event)
    {
        TrainingExercises.Add(@event.Training);

        return this;
    }

    public TrainingDetails TrainingExerciseRemoved(ExerciseRemoved @event)
    {
        var exercise = TrainingExercises.First(_ => _.Id == @event.ExerciseId);
        TrainingExercises.Remove(exercise);

        return this;
    }

}