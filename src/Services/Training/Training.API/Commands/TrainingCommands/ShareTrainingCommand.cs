using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record ShareTrainingCommand(Guid TrainingId) : ICommand<Unit>
{
    public static ShareTrainingCommand Create(ShareTrainingRequest request)
    {
        return new ShareTrainingCommand(request.TrainingId);
    }
}