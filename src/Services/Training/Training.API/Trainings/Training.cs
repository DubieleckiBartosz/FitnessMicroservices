using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events;
using Training.API.Trainings.Enums;
using Training.API.Trainings.ReadModels;
using Training.API.Trainings.TrainingEvents;

namespace Training.API.Trainings;

public class Training : AggregateRoot
{
    public bool IsActive { get; private set; }
    public decimal? Price { get; private set; }
    public TrainingStatus Status { get; private set; }
    public TrainingAvailability Availability { get; private set; }
    public TrainingType? Type { get; private set; }
    public int? DurationTrainingInMinutes { get; private set; }
    public int? BreakBetweenExercisesInMinutes { get; private set; }
    public DateTime Created { get; private set; }
    public Guid TrainerId { get; private set; }
    public List<TrainingExercise> TrainingExercises { get; private set; }
    public List<TrainingUser> TrainingUsers { get; private set; }

    public Training()
    {
    }

    private Training(Guid trainerId)
    {
    
        var @event = NewTrainingInitiated.Create(trainerId, Guid.NewGuid(), DateTime.UtcNow);
        this.Apply(@event);
        this.Enqueue(@event);
    }

    public void AddUser(TrainingUser user)
    {
        var userAlreadyAdded = TrainingUsers.Any(_ => _.Id == user.Id);
        if (userAlreadyAdded) 
        {
            //Throw Business Exception
        }

        var @event = UserToTrainingAdded.Create(user.Id, this.Id);
        this.Apply(@event);
        this.Enqueue(@event);
    }

    public void AddExercise(int numberRepetitions, int breakBetweenSetsInMinutes, Guid externalExerciseId)
    {
        var exerciseAlreadyAdded = TrainingExercises.Any(_ => _.ExternalExerciseId == externalExerciseId);
        if (exerciseAlreadyAdded)
        {
            //Throw Business Exception
        }

        if (!TrainingExercises.Any())
        {
            Status = TrainingStatus.InProgress;
        }

        var exercise =
            TrainingExercise.CreateExercise(externalExerciseId, numberRepetitions, breakBetweenSetsInMinutes);
        var @event = ExerciseAdded.Create(exercise);
        Apply(@event);
        Enqueue(@event);
    }

    public void RemoveExercise(Guid exerciseId)
    {
        var exercise = FindExercise(exerciseId);
        if (exercise == null)
        {
            //Throw Business Exception
        }

        var @event = ExerciseRemoved.Create(exercise.Id);
        Apply(@event);
        this.Enqueue(@event);
    }

    public static Training Create(Guid trainerId)
    {
        return new Training(trainerId);
    }

    protected override void When(IEvent @event)
    {
        switch (@event)
        {
            case NewTrainingInitiated e:
                Initiated(e);
                break;
            case UserToTrainingAdded e:
                UserAdded(e);
                break;
            case ExerciseAdded e:
                NewExerciseAdded(e);
                break;
            case ExerciseRemoved e:
                TrainingExerciseRemoved(e);
                break;
            default:
                break; 
        }
    }

    public void Initiated(NewTrainingInitiated @event)
    {
        Id = @event.TrainingId;
        IsActive = false;
        Status = TrainingStatus.shared;
        Availability = TrainingAvailability.Private;
        Created = @event.Created;
        TrainerId = @event.TrainerId;
        TrainingExercises = new List<TrainingExercise>();
        TrainingUsers = new List<TrainingUser>();
    }

    public void UserAdded(UserToTrainingAdded @event)
    {
        var user = TrainingUsers.First(_ => _.Id == @event.UserId);
        TrainingUsers.Remove(user);
    }

    public void NewExerciseAdded(ExerciseAdded @event)
    {
        TrainingExercises.Add(@event.Training);
    }

    public void TrainingExerciseRemoved(ExerciseRemoved @event)
    {
        TrainingExercises.Remove(FindExercise(exerciseId: @event.ExerciseId));
    }

    private TrainingExercise? FindExercise(Guid exerciseId) => TrainingExercises.FirstOrDefault(_ => _.Id == exerciseId);
}