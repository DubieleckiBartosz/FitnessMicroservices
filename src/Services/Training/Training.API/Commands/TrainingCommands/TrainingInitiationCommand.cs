using Fitness.Common.Abstractions;
using MediatR;

namespace Training.API.Commands.TrainingCommands
{
    public record TrainingInitiationCommand() : ICommand<Unit>
    {
    }
}
