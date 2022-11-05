using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Training.API.Commands.TrainerCommands;
using Training.API.Constants;
using Training.API.Repositories.Interfaces;
using Training.API.Trainings.ReadModels;

namespace Training.API.Handlers.TrainerHandlers;

public class AddNewTrainerInfoHandler : ICommandHandler<AddNewTrainerInfoCommand, Guid>
{
    private readonly IWrapperRepository _wrapperRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ITrainerRepository _trainerRepository;

    public AddNewTrainerInfoHandler(IWrapperRepository wrapperRepository, ICurrentUser currentUser)
    {
        _wrapperRepository = wrapperRepository;
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _trainerRepository = wrapperRepository?.TrainerRepository ?? throw new ArgumentNullException(nameof(wrapperRepository));
    }
    public async Task<Guid> Handle(AddNewTrainerInfoCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.UserId;
        var result = await _trainerRepository.GetTrainerByUserIdAsync(currentUserId);
        if (result != null)
        {
            throw new NotFoundException(Strings.TrainerNotFoundMessage, Strings.TrainerNotFoundTitle);
        }

        var newTrainer = TrainerInfo.Create(request.YearsExperience, request.TrainerName, currentUserId);
        await _trainerRepository.AddNewTrainerInfoAsync(newTrainer);
        await _wrapperRepository.SaveAsync(cancellationToken);

        return newTrainer.Id;
    }
}