﻿using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using Fitness.Common.Extensions;
using MediatR;

namespace Training.API.Handlers;

public class ShareTrainingHandler : ICommandHandler<ShareTrainingCommand, Unit>
{
    private readonly IRepository<Trainings.Training> _repository;
    private readonly ICurrentUser _currentUser;

    public ShareTrainingHandler(IRepository<Trainings.Training> repository, ICurrentUser currentUser)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }
    public async Task<Unit> Handle(ShareTrainingCommand request, CancellationToken cancellationToken)
    {
        var trainingResult = await _repository.GetAsync(request.TrainingId);
        if (trainingResult == null || trainingResult.Id == default)
        {
            throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
        }

        var trainerCode = _currentUser.TrainerCode;
        if (trainingResult.TrainerUniqueCode != trainerCode.ParseToGuidOrThrow())
        {
            throw new BadRequestException(Strings.TrainerCodeDoesNotMatchToTrainingMessage, Strings.IncorrectTrainerCodeTitle);
        }

        trainingResult.Share(request.IsPublic);
        if (request.IsPublic)
        {
            await _repository.AddAsync(trainingResult);
        }
        else
        {
            await _repository.AddAndPublishAsync(trainingResult);
        }

        return Unit.Value;
    }
}