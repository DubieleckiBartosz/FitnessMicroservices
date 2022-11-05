using Fitness.Common.Abstractions; 

namespace Training.API.Commands.TrainingCommands
{
    public record TrainingInitiationCommand(int UserId) : ICommand<Guid>
    {
        public static TrainingInitiationCommand Create(int userId) 
        {
            return new TrainingInitiationCommand(userId);
        }
    }
}
