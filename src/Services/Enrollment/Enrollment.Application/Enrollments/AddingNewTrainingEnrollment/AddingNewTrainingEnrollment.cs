using Enrollment.Application.Commands;
using Enrollment.Application.Constants;
using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;

public class AddingNewTrainingEnrollment : ICommandHandler<NewTrainingEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _enrollmentRepository;
    private readonly ICurrentUser _currentUser;

    public AddingNewTrainingEnrollment(IRepository<Enrollment> enrollmentRepository, ICurrentUser currentUser)
    {
        _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<Unit> Handle(NewTrainingEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new NotFoundException(Strings.EnrollmentNotFoundMessage, Strings.EnrollmentNotFoundTitle);
        }

        var userId = _currentUser.UserId;
        enrollment.NewUserTrainingEnrollment(userId);
        await _enrollmentRepository.AddAsync(enrollment);

        return Unit.Value;
    }
}