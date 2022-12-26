using Fitness.Common.Extensions; 

namespace Training.API.Trainings.ReadModels;

public class TrainingDetails : ITrainingRead
{
    private int NumberUsersEnrolled => TrainingUsers.Count;
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public Guid? EnrollmentId { get; private set; }
    public Guid TrainerUniqueCode { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsHistoric { get; private set; }
    public decimal? Price { get; private set; }
    public TrainingStatus Status { get; private set; }
    public TrainingAvailability Availability { get; private set; }
    public TrainingType? Type { get; private set; }
    public int? DurationTrainingInMinutes { get; private set; }
    public int? BreakBetweenExercisesInMinutes { get; private set; }
    public DateTime Created { get; private set; }
    public List<TrainingExercise> TrainingExercises { get; private set; }
    public List<TrainingUser> TrainingUsers { get; private set; }

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
        TrainingUsers.Add(user);

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
        var result = TrainingExercises.First(_ => _.Id == @event.ExerciseId); 

        if (result.NumberRepetitions == @event.NumberRepetitions)
        {
            TrainingExercises.Remove(result);
        }
        else
        {
            TrainingExercises.Replace(result,
                result.Update(@event.NumberRepetitions, null));
        }

        return this;
    }

    public TrainingDetails Shared(TrainingShared @event)
    {
        Status = TrainingStatus.Shared;
        Availability = @event.IsPublic ? TrainingAvailability.Public : TrainingAvailability.Group;
        IsActive = true;

        return this;
    }

    public TrainingDetails TrainingAvailabilityChanged(AvailabilityChanged @event)
    {
        Availability = @event.NewAvailability;

        return this;
    }
    public void MarkedAsHistory(TrainingMarkedAsHistorical @event)
    {
        IsHistoric = true;
    }
    public void EnrollmentIdAssigned(EnrollmentAssigned @event)
    {
        EnrollmentId = @event.EnrollmentId;
    }

    public void Updated(TrainingDataUpdated @event)
    {
        DurationTrainingInMinutes = @event.DurationTrainingInMinutes ?? DurationTrainingInMinutes;
        BreakBetweenExercisesInMinutes = @event.BreakBetweenExercisesInMinutes ?? BreakBetweenExercisesInMinutes;
        Price = @event.Price ?? Price;
    }

    public int GetUsersEnrolledCount() => NumberUsersEnrolled;
}