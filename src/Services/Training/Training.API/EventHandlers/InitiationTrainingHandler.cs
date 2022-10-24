using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Repository;
using Training.API.Commands.TrainingCommands;

namespace Training.API.EventHandlers
{
    public class InitiationTrainingHandler : ICommandHandler<TrainingInitiationCommand, Guid>
    {
        private readonly IRepository<Trainings.Training> _trainingRepository;

        public InitiationTrainingHandler(IRepository<Trainings.Training> trainingRepository)
        {
            _trainingRepository = trainingRepository ?? throw new ArgumentNullException(nameof(trainingRepository));
        }
        public async Task<Guid> Handle(TrainingInitiationCommand request, CancellationToken cancellationToken)
        {
            var initiation = Trainings.Training.Create(request.TrainerId);
            await _trainingRepository.AddAsync(initiation);

            return initiation.Id;
        }
    }
}
