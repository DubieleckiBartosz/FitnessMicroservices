using Enrollment.Application.Commands; 
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

public class StartingEnrollments : ICommandHandler<StartEnrollmentsCommand, Unit>
{
    private readonly IRepository<Enrollment> _repository;

    public StartingEnrollments(IRepository<Enrollment> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(StartEnrollmentsCommand request, CancellationToken cancellationToken)
    {
        var newEnrollment = Enrollment.Create(request.TrainingId, request.Creator);
        await _repository.AddAsync(newEnrollment);

        return Unit.Value;
    }
}