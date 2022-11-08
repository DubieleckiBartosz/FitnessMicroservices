using MediatR;

namespace Training.API.Commands.ExerciseCommands;

public record RemoveExerciseCommand(Guid TrainingId, Guid ExerciseId, int NumberRepetitions) : ICommand<Unit>
{
    public static RemoveExerciseCommand Create(RemoveExerciseRequest request)
    {
        return new RemoveExerciseCommand(request.TrainingId, request.ExerciseId, request.NumberRepetitions);
    }
}