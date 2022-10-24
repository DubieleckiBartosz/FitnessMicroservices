using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions; 
using Fitness.Common.EventStore.Repository;
using MediatR;
using Training.API.Commands.ExerciseCommands;
using Training.API.Constants; 

namespace Training.API.EventHandlers;

public class AddExerciseToTrainingHandler : ICommandHandler<AddExerciseCommand, Unit>
{
    private readonly IRepository<Trainings.Training> _repository; 

    public AddExerciseToTrainingHandler(IRepository<Trainings.Training> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository)); 
    }

    public async Task<Unit> Handle(AddExerciseCommand request, CancellationToken cancellationToken)
    {
        //Check exercise

        var trainingResult = await _repository.GetAsync(request.TrainingId);
        if (trainingResult == null)
        {
            throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
        }

        trainingResult.AddExercise(request.NumberRepetitions, request.BreakBetweenSetsInMinutes,
            request.ExternalExerciseId);

        await _repository.UpdateAsync(trainingResult);

        return Unit.Value;
    }
}