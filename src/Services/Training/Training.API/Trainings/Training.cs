using Fitness.Common.Extensions;
using Training.API.Common.Exceptions;

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
    public Guid TrainerUniqueCode { get; private set; }
    public List<TrainingExercise> TrainingExercises { get; private set; }
    public List<TrainingUser> TrainingUsers { get; private set; }

    public Training()
    {
    }

    private Training(Guid trainerUniqueCode)
    {
    
        var @event = NewTrainingInitiated.Create(trainerUniqueCode, Guid.NewGuid(), DateTime.UtcNow);
        this.Apply(@event);
        this.Enqueue(@event);
    }

    public void AddUser(TrainingUser user)
    {
        var userAlreadyAdded = TrainingUsers.Any(_ => _.Id == user.Id);
        if (userAlreadyAdded)
        {
            throw new TrainingServiceBusinessException(Strings.UserDuplicationTitle, Strings.UserDuplicationMessage); 
        }

        var @event = UserToTrainingAdded.Create(user.Id, this.Id);
        this.Apply(@event);
        this.Enqueue(@event);
    }
     
    public void AddExercise(int numberRepetitions, int breakBetweenSetsInMinutes, Guid externalExerciseId)
    {
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
            throw new TrainingServiceBusinessException(Strings.ExerciseNotFoundTitle, Strings.ExerciseNotFoundMessage);
        }

        var @event = ExerciseRemoved.Create(exercise.Id, this.Id);
        Apply(@event);
        this.Enqueue(@event);
    }

    public static Training Create(Guid trainerUniqueCode)
    {
        return new Training(trainerUniqueCode);
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
        Status = TrainingStatus.Init;
        Availability = TrainingAvailability.Private;
        Created = @event.Created;
        TrainerUniqueCode = @event.TrainerUniqueCode;
        TrainingExercises = new List<TrainingExercise>();
        TrainingUsers = new List<TrainingUser>();
    }

    public void UserAdded(UserToTrainingAdded @event)
    {
        var user = TrainingUsers.First(_ => _.Id == @event.UserId);
        TrainingUsers.Add(user);
    }

    public void NewExerciseAdded(ExerciseAdded @event)
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
    }

    public void TrainingExerciseRemoved(ExerciseRemoved @event)
    {
        var result = FindExercise(exerciseId: @event.ExerciseId);
        if (result == null)
        {
            throw new TrainingServiceBusinessException(Strings.ExerciseNotFoundTitle, Strings.ExerciseNotFoundMessage);
        }

        TrainingExercises.Remove(result);
    }

    private TrainingExercise? FindExercise(Guid exerciseId) => TrainingExercises.FirstOrDefault(_ => _.Id == exerciseId);
}