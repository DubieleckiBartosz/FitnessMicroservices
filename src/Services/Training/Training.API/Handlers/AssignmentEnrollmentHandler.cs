using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Training.API.Handlers;

public class AssignmentEnrollmentHandler : ICommandHandler<AssignmentEnrollmentCommand, Unit>
{
    private readonly IRepository<Trainings.Training> _trainingRepository;

    public AssignmentEnrollmentHandler(IRepository<Trainings.Training> trainingRepository)
    {
        _trainingRepository = trainingRepository ?? throw new ArgumentNullException(nameof(trainingRepository));
    }
    public async Task<Unit> Handle(AssignmentEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetAsync(request.TrainingId);
        if (training == null || training.Id == default)
        {
            throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
        }

        training.AssignEnrollment(enrollmentId: request.Enrollment);
        await _trainingRepository.UpdateAsync(training);

        return Unit.Value;
    }
}