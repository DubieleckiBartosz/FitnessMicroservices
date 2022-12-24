using Enrollment.Application.Commands;
using Enrollment.Application.Constants;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.SuspensionEnrollment;

public class SuspensionEnrollment : ICommandHandler<SuspensionEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _repository;

    public SuspensionEnrollment(IRepository<Enrollment> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(SuspensionEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new NotFoundException(Strings.EnrollmentNotFoundMessage, Strings.EnrollmentNotFoundTitle);
        }

        enrollment.Suspend(request.SuspensionBy);

        await _repository.UpdateAsync(enrollment);

        return Unit.Value;
    }
}