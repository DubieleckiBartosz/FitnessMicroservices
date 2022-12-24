using Enrollment.Application.Commands;
using Enrollment.Application.Constants;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.ClosingEnrollment;

public class ClosingEnrollment : ICommandHandler<CloseEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public ClosingEnrollment(IRepository<Enrollment> enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
    }

    public async Task<Unit> Handle(CloseEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new NotFoundException(Strings.EnrollmentNotFoundMessage, Strings.EnrollmentNotFoundTitle);
        }

        enrollment.Close(request.CloseBy);
        await _enrollmentRepository.UpdateAsync(enrollment);

        return Unit.Value;
    }
}