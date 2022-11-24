using Enrollment.Application.Commands;
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

public class StartingEnrollments : ICommandHandler<StartEnrollmentsCommand, Unit>
{
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public StartingEnrollments(IRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
    }

    public async Task<Unit> Handle(StartEnrollmentsCommand request, CancellationToken cancellationToken)
    {
        var newEnrollment = Enrollment.Create(request.TrainingId);
        await _enrollmentRepository.AddAsync(newEnrollment);

        return Unit.Value;
    }
}