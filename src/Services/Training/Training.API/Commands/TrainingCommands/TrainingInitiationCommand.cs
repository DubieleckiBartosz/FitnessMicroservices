namespace Training.API.Commands.TrainingCommands
{
    public record TrainingInitiationCommand() : ICommand<Guid>
    {
        public static TrainingInitiationCommand Create() 
        {
            return new TrainingInitiationCommand();
        }
    }
}
