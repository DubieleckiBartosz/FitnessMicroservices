using Fitness.Common.Abstractions;
using MediatR;

namespace Training.API.Commands.ExerciseCommands
{
    public record RemoveExerciseCommand(Guid TrainingId, Guid ExerciseId) : ICommand<Unit>
    {
        public static RemoveExerciseCommand Create(Guid trainingId, Guid exerciseId)
        {
            return new RemoveExerciseCommand(trainingId, exerciseId);
        }
    }
}
