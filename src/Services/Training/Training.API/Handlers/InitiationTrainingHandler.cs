using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using Training.API.Commands.TrainingCommands;
using Training.API.Constants;
using Training.API.Repositories.Interfaces;

namespace Training.API.Handlers;

public class InitiationTrainingHandler : ICommandHandler<TrainingInitiationCommand, Guid>
{
    private readonly IRepository<Trainings.Training> _trainingRepository;
    private readonly ITrainerRepository _trainerRepository;

    public InitiationTrainingHandler(IRepository<Trainings.Training> trainingRepository,
        IWrapperRepository wrapperRepository)
    {
        _trainingRepository = trainingRepository ?? throw new ArgumentNullException(nameof(trainingRepository));
        _trainerRepository = wrapperRepository?.TrainerRepository ?? throw new ArgumentNullException(nameof(wrapperRepository));
    }

    public async Task<Guid> Handle(TrainingInitiationCommand request, CancellationToken cancellationToken)
    {
        var trainer = await _trainerRepository.GetTrainerByUserIdAsync(request.UserId);
        if (trainer == null)
        {
            throw new NotFoundException(Strings.TrainerNotFoundMessage, Strings.TrainerNotFoundTitle);
        }

        var initiation = Trainings.Training.Create(trainer.Id);
        await _trainingRepository.AddAsync(initiation);

        return initiation.Id;
    }
}