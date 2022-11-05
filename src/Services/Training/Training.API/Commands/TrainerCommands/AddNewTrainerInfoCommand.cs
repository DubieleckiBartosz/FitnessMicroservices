using Fitness.Common.Abstractions;
using Training.API.Requests.TrainerRequests;

namespace Training.API.Commands.TrainerCommands;

public record AddNewTrainerInfoCommand(string TrainerName, int YearsExperience) : ICommand<Guid>
{
    public static AddNewTrainerInfoCommand Create(AddNewTrainerInfoRequest request)
    {
        return new AddNewTrainerInfoCommand(request.TrainerName, request.YearsExperience);
    }
}