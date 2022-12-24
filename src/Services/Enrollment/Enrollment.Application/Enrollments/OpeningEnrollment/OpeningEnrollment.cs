using Enrollment.Application.Commands;
using Enrollment.Application.Constants;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.EventStore.Repository;
using MediatR;

namespace Enrollment.Application.Enrollments.OpeningEnrollment;

public class OpeningEnrollment : ICommandHandler<OpenEnrollmentCommand, Unit>
{
    private readonly IRepository<Enrollment> _repository;

    public OpeningEnrollment(IRepository<Enrollment> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(OpenEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _repository.GetAsync(request.EnrollmentId);
        if (enrollment == null)
        {
            throw new NotFoundException(Strings.EnrollmentNotFoundMessage, Strings.EnrollmentNotFoundTitle);
        }

        enrollment.Open(request.OpenBy);

        await _repository.UpdateAsync(enrollment);
        
        return Unit.Value;
    }
}