using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;

namespace Training.API.Handlers;

public class InitiationTrainingHandler : ICommandHandler<TrainingInitiationCommand, Guid>
{
    private readonly IRepository<Trainings.Training> _trainingRepository;
    private readonly ICurrentUser _currentUser;

    public InitiationTrainingHandler(IRepository<Trainings.Training> trainingRepository, ICurrentUser currentUser)
    {
        _trainingRepository = trainingRepository ?? throw new ArgumentNullException(nameof(trainingRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Guid> Handle(TrainingInitiationCommand request, CancellationToken cancellationToken)
    {
        var trainerUniqueCode = _currentUser.TrainerCode;
        if (!Guid.TryParse(trainerUniqueCode, out var resultTrainerUniqueCode))
        {
            throw new NotFoundException(Strings.IncorrectTrainerCodeMessage, Strings.IncorrectTrainerCodeTitle);
        }
                    
        var initiation = Trainings.Training.Create(resultTrainerUniqueCode);
        await _trainingRepository.AddAsync(initiation);

        return initiation.Id;
    }
}