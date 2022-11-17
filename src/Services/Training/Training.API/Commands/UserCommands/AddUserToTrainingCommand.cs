using MediatR;

namespace Training.API.Commands.UserCommands;

public record AddUserToTrainingCommand(Guid UserId, Guid TrainingId) : ICommand<Unit>
{
    public static AddUserToTrainingCommand Create(AddUserToTrainingRequest request)
    {
        return new AddUserToTrainingCommand(request.UserId, request.TrainingId);
    }
}