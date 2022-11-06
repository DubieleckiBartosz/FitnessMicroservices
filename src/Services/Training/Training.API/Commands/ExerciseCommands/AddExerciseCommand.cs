using MediatR;

namespace Training.API.Commands.ExerciseCommands
{
    public record AddExerciseCommand(int NumberRepetitions, int BreakBetweenSetsInMinutes, Guid TrainingId, Guid ExternalExerciseId) : ICommand<Unit>
    {
        public static AddExerciseCommand Create(AddExerciseRequest request)
        {
            return new AddExerciseCommand(request.NumberRepetitions, request.BreakBetweenSetsInMinutes,
                request.TrainingId, request.ExternalExerciseId);
        }
    }
}
