using MediatR;

namespace Training.API.Commands.TrainingCommands;

public record NewAvailabilityCommand(Guid TrainingId, TrainingAvailability NewAvailability) : ICommand<Unit>
{
    public static NewAvailabilityCommand Create(NewAvailabilityRequest request)
    {
        return new NewAvailabilityCommand(request.TrainingId, request.NewAvailability);
    }
}