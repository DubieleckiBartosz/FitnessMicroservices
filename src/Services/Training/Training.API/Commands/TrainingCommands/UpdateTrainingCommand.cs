using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record UpdateTrainingCommand(Guid TrainingId, int? DurationTrainingInMinutes,
    int? BreakBetweenExercisesInMinutes, decimal? Price) : ICommand<Unit>
{
    public static UpdateTrainingCommand Create(UpdateTrainingRequest request)
    {
        return new UpdateTrainingCommand(request.TrainingId, request.DurationTrainingInMinutes,
            request.BreakBetweenExercisesInMinutes, request.Price);
    }
}