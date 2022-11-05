﻿using Fitness.Common.Core.Exceptions;
using Fitness.Common.Projection;
using Training.API.Constants;
using Training.API.Repositories.Interfaces;
using Training.API.Trainings.ReadModels;
using Training.API.Trainings.TrainingEvents;

namespace Training.API.Trainings.TrainingProjections;

public class TrainingDetailsProjection : ReadModelAction<TrainingDetails>
{
    private readonly IWrapperRepository _wrapperRepository;
    private readonly ITrainingRepository _trainingRepository; 

    public TrainingDetailsProjection(IWrapperRepository? wrapperRepository) 
    {
        _trainingRepository = wrapperRepository?.TrainingRepository ?? throw new ArgumentNullException(nameof(wrapperRepository));
        _wrapperRepository = wrapperRepository;
        Projects<NewTrainingInitiated>(Handle);
        Projects<UserToTrainingAdded>(Handle);
        Projects<ExerciseAdded>(Handle);
        Projects<ExerciseRemoved>(Handle);
    }

    private async Task Handle(NewTrainingInitiated @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var newTraining = TrainingDetails.Create(@event);
        await _trainingRepository.CreateAsync(newTraining, cancellationToken);
        await _wrapperRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(UserToTrainingAdded @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var training = await GetTrainingDetails(@event.TrainingId, cancellationToken);

        training.UserAdded(@event);

        _trainingRepository.Update(training);
        await _wrapperRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(ExerciseAdded @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var training = await GetTrainingDetails(@event.TrainingId, cancellationToken);

        training.NewExerciseAdded(@event);

        _trainingRepository.Update(training);
        await _wrapperRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(ExerciseRemoved @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var training = await GetTrainingDetails(@event.TrainingId, cancellationToken);
        training.TrainingExerciseRemoved(@event);

        _trainingRepository.Update(training);
        await _wrapperRepository.SaveAsync(cancellationToken);
    }

    private async Task<TrainingDetails> GetTrainingDetails(Guid trainingId, CancellationToken cancellationToken = default)
    {
        var training = await _trainingRepository.GetTrainingDetailsAsync(trainingId, cancellationToken);

        if (training == null)
        {
            throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
        }

        return training;
    }
}