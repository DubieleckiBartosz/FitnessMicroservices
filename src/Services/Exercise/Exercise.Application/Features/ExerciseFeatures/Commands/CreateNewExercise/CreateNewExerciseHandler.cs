using Exercise.Application.Contracts;
using Exercise.Domain.Base;
using Exercise.Domain.ValueObjects;
using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.CreateNewExercise;

public class CreateNewExerciseHandler : ICommandHandler<CreateNewExerciseCommand, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICurrentUser _currentUser;

    public CreateNewExerciseHandler(IExerciseRepository exerciseRepository, ICurrentUser currentUser)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Guid> Handle(CreateNewExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByNameAsync(request.Name);
        if (exercise != null)
        {
            throw new ExerciseBusinessException("There is already an exercise with the same name", "Bad name");
        }

        var userCreator = _currentUser.Email;
        var newExercise = Domain.Entities.Exercise.Create(request.Name, userCreator, ExerciseDescription.Create(request.Description));

        await _exerciseRepository.AddAsync(newExercise);

        return newExercise.Id;
    }
}