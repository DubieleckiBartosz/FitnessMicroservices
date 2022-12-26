namespace Training.API.Constants;

public class Strings
{
    //Roles
    public const string TrainerRole = "Trainer";

    //Errors
    public const string TrainingNotFoundMessage = "Training cannot be null.";
    public const string IncorrectTrainerCodeMessage = "Failed to parse trainer code.";

    public const string TrainerCodeDoesNotMatchToTrainingMessage =
        "You are not authorized to add an exercise to this training.";

    public const string BadTrainerCodeMessage = "You are not authorized for this training.";

    public const string UserDuplicationMessage = "One user cannot have the same training twice.";
    public const string ExerciseDuplicationMessage = "The exercise already exists, update the existing exercise.";
    public const string ExerciseNotFoundMessage = "The exercise has not been assigned to a training.";
    public const string TrainingBadStatusMessage = "The current training status does not allow you to change to the desired status.";
    public const string TrainingBadAvailabilityStatusMessage = "Availability cannot be changed to the same.";
    public const string TrainingCannotBePrivateMessage = "Availability cannot be private when some users are using training.";
    public const string TrainingCannotBeHistoricMessage = "If the training is to be history, the list of users must be empty.";
    public const string StatusMustBeSharedMessage = "Status must be 'Shared'.";
    public const string EnrollmentIdCannotBeNullMessage = "EnrollmentId must exist if you want to close enrollment.";
    public const string PriceCannotBeNull = "The price cannot be null during the first data additions.";
    public const string BreakBetweenExercisesCannotBeNull = "The break between exercises cannot be null during the first data additions.";
    public const string DurationTrainingCannotBeNull = "The duration cannot be null during the first data additions.";


    //Titles
    public const string BadTrainerCodeTitle = "Bad trainer code.";
    public const string TrainingNotFoundTitle = "Training not found.";
    public const string ApiExceptionTitle = "Training API Exception";
    public const string IncorrectTrainerCodeTitle = "Bad code.";
    public const string UserDuplicationTitle = "User duplication.";
    public const string TrainingBadStatusTitle = "Not valid status.";
    public const string ExerciseDuplicationTitle = "Exercise duplication.";
    public const string ExerciseNotFoundTitle = "Exercise not found.";
    public const string TrainingTheSameAvailabilityTitle = "The availability is the same.";
    public const string TrainingCannotBePrivateTitle = "The availability cannot be private.";
    public const string TrainingCannotBeHistoricTitle = "The user list must be empty.";
    public const string StatusMustBeSharedTitle = "Bad status.";
    public const string EnrollmentIdCannotBeNullTitle = "EnrollmentId cannot be null.";
    public const string DataMustBeCompleted = "Data must be completed.";
}