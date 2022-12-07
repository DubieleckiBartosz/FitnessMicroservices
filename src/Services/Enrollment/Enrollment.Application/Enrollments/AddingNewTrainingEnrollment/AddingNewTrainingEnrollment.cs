using Enrollment.Application.Commands;
using Fitness.Common.Abstractions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;

public class AddingNewTrainingEnrollment : ICommandHandler<NewTrainingEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public AddingNewTrainingEnrollment(IRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
    }

    public async Task<Unit> Handle(NewTrainingEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var result = await _enrollmentRepository.GetAsync(request.EnrollmentId);
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        result.NewUserTrainingEnrollment(request.UserId);
        await _enrollmentRepository.AddAsync(result);

        return Unit.Value;
    }
}