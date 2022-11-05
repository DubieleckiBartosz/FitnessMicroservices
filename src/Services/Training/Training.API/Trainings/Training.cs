using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events;
using Training.API.Trainings.Enums;
using Training.API.Trainings.ReadModels;
using Training.API.Trainings.TrainingEvents;

namespace Training.API.Trainings;

public class Training : Aggregate
{
    public bool IsActive { get; private set; }
    public decimal? Price { get; private set; }
    public TrainingStatus Status { get; private set; }
    public TrainingAvailability Availability { get; private set; }
    public TrainingType? Type { get; private set; }
    public int? DurationTrainingInMinutes { get; private set; }
    public int? BreakBetweenExercisesInMinutes { get; private set; }
    public DateTime Created { get; private set; }
    public Guid CreatorId { get; private set; }
    public List<TrainingExercise> TrainingExercises { get; private set; }
    public List<TrainingUser> TrainingUsers { get; private set; }

    public Training()
    {
    }

    private Training(Guid creatorId)
    {
    
        var @event = NewTrainingInitiated.Create(creatorId, Guid.NewGuid(), DateTime.UtcNow);
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
        var @event = ExerciseAdded.Create(exercise, this.Id);
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

        var @event = ExerciseRemoved.Create(exercise.Id, this.Id);
        Apply(@event);
        this.Enqueue(@event);
    }

    public static Training Create(Guid creatorId)
    {
        return new Training(creatorId);
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
        Status = TrainingStatus.Created;
        Availability = TrainingAvailability.Private;
        Created = @event.Created;
        CreatorId = @event.CreatorId;
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
        var result = FindExercise(exerciseId: @event.ExerciseId);
        if (result != null)
        {
            //Throw Business Exception
        }

        TrainingExercises.Remove(result);
    }

    private TrainingExercise? FindExercise(Guid exerciseId) => TrainingExercises.FirstOrDefault(_ => _.Id == exerciseId);
}