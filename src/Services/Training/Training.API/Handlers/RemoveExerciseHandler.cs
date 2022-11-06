using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Training.API.Handlers
{
    public class RemoveExerciseHandler : ICommandHandler<RemoveExerciseCommand, Unit>
    {
        private readonly IRepository<Trainings.Training> _repository;

        public RemoveExerciseHandler(IRepository<Trainings.Training> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Unit> Handle(RemoveExerciseCommand request, CancellationToken cancellationToken)
        {
            var trainingResult = await _repository.GetAsync(request.TrainingId);
            if (trainingResult == null)
            {
                throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
            }

            trainingResult.RemoveExercise(request.ExerciseId);

            await _repository.UpdateAndPublishAsync(trainingResult);
            return Unit.Value;
        }
    }
}
