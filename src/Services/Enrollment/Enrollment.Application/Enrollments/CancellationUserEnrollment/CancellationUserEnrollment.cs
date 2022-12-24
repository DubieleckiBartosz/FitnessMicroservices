using Enrollment.Application.Commands;
using Enrollment.Application.Constants; 
using Fitness.Common.Abstractions;
using Fitness.Common.CommonServices;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using Fitness.Common.Extensions;
using MediatR;

namespace Enrollment.Application.Enrollments.CancellationUserEnrollment;

public class CancellationUserEnrollment : ICommandHandler<CancelUserEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _repository;
    private readonly ICurrentUser _currentUser;

    public CancellationUserEnrollment(IRepository<Enrollment> repository, ICurrentUser currentUser)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }
    public async Task<Unit> Handle(CancelUserEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new NotFoundException(Strings.EnrollmentNotFoundMessage, Strings.EnrollmentNotFoundTitle);
        }

        var trainer = _currentUser.TrainerCode;

        var user = _currentUser.UserId;

        enrollment.CancelUserEnrollment(request.UserEnrollment, user, trainer?.ParseToGuidOrThrow());
        await _repository.UpdateAsync(enrollment);

        return Unit.Value;
    }
}