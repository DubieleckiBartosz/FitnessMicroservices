using Fitness.Common.Extensions;
using Training.API.Common.Exceptions;

namespace Training.API.Trainings;

public class Training : Aggregate
{
    public bool IsActive { get; private set; }
    public decimal? Price { get; private set; }
    public Guid? EnrollmentId { get; private set; }
    public TrainingStatus Status { get; private set; }
    public TrainingAvailability Availability { get; private set; }
    public TrainingType? Type { get; private set; }
    public int? DurationTrainingInMinutes { get; private set; }
    public int? BreakBetweenExercisesInMinutes { get; private set; }
    public DateTime Created { get; private set; }
    public Guid TrainerUniqueCode { get; private set; }
    public bool IsHistoric { get; private set; }
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

    public void ChangeAvailability(TrainingAvailability newAvailability, Guid trainerUniqueCode)
    {  
        if (!IsCreator(trainerUniqueCode))
        {
            throw new TrainingServiceBusinessException(Strings.BadTrainerCodeTitle, Strings.BadTrainerCodeMessage);
        }

        if (Availability == newAvailability)
        {
            throw new TrainingServiceBusinessException(Strings.TrainingTheSameAvailabilityTitle, Strings.TrainingBadAvailabilityStatusMessage);
        }

        if (newAvailability == TrainingAvailability.Private && TrainingUsers.Any())
        {
            throw new TrainingServiceBusinessException(Strings.TrainingCannotBePrivateTitle, Strings.TrainingCannotBePrivateMessage);
        }

        var @event = AvailabilityChanged.Create(this.Id, this.EnrollmentId, newAvailability, this.TrainerUniqueCode);
        this.Apply(@event);
        this.Enqueue(@event); 
    }

    public void AssignEnrollment(Guid enrollmentId)
    {
        var @event = EnrollmentAssigned.Create(this.Id, enrollmentId);
        this.Apply(@event);
        this.Enqueue(@event);
    }

    public void Update(Guid trainerUniqueCode, int? durationTrainingInMinutes, int? breakBetweenExercisesInMinutes, decimal? price)
    {
        if (!IsCreator(trainerUniqueCode))
        {
            throw new TrainingServiceBusinessException(Strings.BadTrainerCodeTitle, Strings.BadTrainerCodeMessage);
        }

        if (durationTrainingInMinutes == null && DurationTrainingInMinutes == null)
        {
            throw new TrainingServiceBusinessException(Strings.DataMustBeCompleted, Strings.DurationTrainingCannotBeNull);
        }

        if (breakBetweenExercisesInMinutes == null && BreakBetweenExercisesInMinutes == null)
        {
            throw new TrainingServiceBusinessException(Strings.DataMustBeCompleted, Strings.BreakBetweenExercisesCannotBeNull);
        }

        if (price == null && Price == null)
        {
            throw new TrainingServiceBusinessException(Strings.DataMustBeCompleted, Strings.PriceCannotBeNull);
        }

        var @event = TrainingDataUpdated.Create(this.Id, durationTrainingInMinutes, breakBetweenExercisesInMinutes, price);
        Apply(@event);
        Enqueue(@event);
    }


    public void ToHistory(Guid trainerUniqueCode)
    {  
        if (!IsCreator(trainerUniqueCode))
        {
            throw new TrainingServiceBusinessException(Strings.BadTrainerCodeTitle, Strings.BadTrainerCodeMessage);
        }

        //If the status is InProgress or Init, it will be deleted automatically (in background)
        if (Status != TrainingStatus.Shared)
        {
            throw new TrainingServiceBusinessException(Strings.StatusMustBeSharedTitle, Strings.StatusMustBeSharedMessage);
        }

        if (TrainingUsers.Any())
        {
            throw new TrainingServiceBusinessException(Strings.TrainingCannotBeHistoricTitle, Strings.TrainingCannotBeHistoricMessage);
        }

        var @event = TrainingMarkedAsHistorical.Create(this.Id, this.EnrollmentId, trainerUniqueCode);
        this.Apply(@event);
        this.Enqueue(@event); 
    }
     
    public void Share(bool isPublic)
    {
        if (Status == TrainingStatus.Shared || (Status == TrainingStatus.Init))
        {
            throw new TrainingServiceBusinessException(Strings.TrainingBadStatusTitle, Strings.TrainingBadStatusMessage);
        }

        var @event = TrainingShared.Create(this.Id, this.TrainerUniqueCode, isPublic);
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

    public void RemoveExercise(Guid exerciseId, int repetitionsToRemove)
    {
        var exercise = FindExercise(exerciseId);
        if (exercise == null)
        {
            throw new TrainingServiceBusinessException(Strings.ExerciseNotFoundTitle, Strings.ExerciseNotFoundMessage);
        }

        var @event = ExerciseRemoved.Create(exercise.Id, this.Id, repetitionsToRemove);
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
            case TrainingDataUpdated e:
                Updated(e);
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
            case TrainingShared e:
                Shared(e);
                break;
            case AvailabilityChanged e:
                TrainingAvailabilityChanged(e);
                break;
            case TrainingMarkedAsHistorical e:
                MarkedAsHistory(e);
                break;
            case EnrollmentAssigned e:
                EnrollmentIdAssigned(e);
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

    public void Updated(TrainingDataUpdated @event)
    { 
        DurationTrainingInMinutes = @event.DurationTrainingInMinutes ?? DurationTrainingInMinutes;
        BreakBetweenExercisesInMinutes = @event.BreakBetweenExercisesInMinutes ?? BreakBetweenExercisesInMinutes;
        Price = @event.Price ?? Price;
    }


    public void UserAdded(UserToTrainingAdded @event)
    {
        var user = TrainingUsers.First(_ => _.Id == @event.UserId);
        TrainingUsers.Add(user);
    }

    public void Shared(TrainingShared @event)
    {
        Status = TrainingStatus.Shared;
        Availability = @event.IsPublic ? TrainingAvailability.Public : TrainingAvailability.Group;
        IsActive = true;
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

        if (result.NumberRepetitions == @event.NumberRepetitions)
        {
            TrainingExercises.Remove(result);
        }
        else
        {
            TrainingExercises.Replace(result,
                result.Update(@event.NumberRepetitions, null));
        }
    }

    public void EnrollmentIdAssigned(EnrollmentAssigned @event)
    {
        EnrollmentId = @event.EnrollmentId;
    }

    public void MarkedAsHistory(TrainingMarkedAsHistorical @event)
    {
        IsHistoric = true;
    }

    public void TrainingAvailabilityChanged(AvailabilityChanged @event)
    {
        Availability = @event.NewAvailability;
    }

    private TrainingExercise? FindExercise(Guid exerciseId) => TrainingExercises.FirstOrDefault(_ => _.Id == exerciseId);
    private bool IsCreator(Guid trainerCode) => TrainerUniqueCode == trainerCode;
}