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

    public const string UserDuplicationMessage = "One user cannot have the same training twice.";
    public const string ExerciseDuplicationMessage = "The exercise already exists, update the existing exercise.";
    public const string ExerciseNotFoundMessage = "The exercise has not been assigned to a training.";

    //Titles
    public const string TrainingNotFoundTitle = "Training not found.";
    public const string ApiExceptionTitle = "Training API Exception";
    public const string IncorrectTrainerCodeTitle = "Bad code.";
    public const string UserDuplicationTitle = "User duplication.";
    public const string ExerciseDuplicationTitle = "Exercise duplication.";
    public const string ExerciseNotFoundTitle = "Exercise not found.";
}