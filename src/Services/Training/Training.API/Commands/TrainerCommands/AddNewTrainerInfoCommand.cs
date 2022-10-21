using Fitness.Common.Abstractions;
using MediatR;

namespace Training.API.Commands.TrainerCommands
{
    public record AddNewTrainerInfoCommand(int UserId, string Email, string UserName) : ICommand<Unit>
    {
    }
}
