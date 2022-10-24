using Fitness.Common.Abstractions; 

namespace Training.API.Commands.TrainingCommands
{
    public record TrainingInitiationCommand(Guid TrainerId) : ICommand<Guid>
    {
        public static TrainingInitiationCommand Create(Guid trainerId)
        {
            return new TrainingInitiationCommand(trainerId);
        }
    }
}
