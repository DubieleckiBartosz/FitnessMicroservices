using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record ShareTrainingCommand(Guid TrainingId, bool IsPublic) : ICommand<Unit>
{
    public static ShareTrainingCommand Create(ShareTrainingRequest request)
    {
        return new ShareTrainingCommand(request.TrainingId, request.IsPublic);
    }
}