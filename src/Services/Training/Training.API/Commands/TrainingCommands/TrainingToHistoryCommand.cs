using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record TrainingToHistoryCommand(Guid TrainingId) : ICommand<Unit>
{
    public static TrainingToHistoryCommand Create(TrainingToHistoryRequest request)
    {
        return new TrainingToHistoryCommand(request.TrainingId);
    }
}