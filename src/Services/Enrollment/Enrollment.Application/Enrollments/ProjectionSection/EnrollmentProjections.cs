using Enrollment.Application.Enrollments.AcceptingUserEnrollment;
using Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;
using Enrollment.Application.Enrollments.CancellationUserEnrollment;
using Enrollment.Application.Enrollments.ClearingUserEnrollmentList;
using Enrollment.Application.Enrollments.ClosingEnrollment;
using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Enrollment.Application.Enrollments.StartingTrainingEnrollments;
using Enrollment.Application.Interfaces;
using Fitness.Common.Projection;

namespace Enrollment.Application.Enrollments.ProjectionSection;

public class EnrollmentProjections : ReadModelAction<TrainingEnrollmentsDetails>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentProjections(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
        Projects<TrainingEnrollmentsStarted>(Handle);
        Projects<UserEnrollmentListCleared>(Handle);
        Projects<UserEnrollmentAccepted>(Handle);
        Projects<UserEnrollmentCancelled>(Handle);
        Projects<EnrollmentClosed>(Handle);
    }

    private async Task Handle(TrainingEnrollmentsStarted @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }
        var trainingEnrollments =
            TrainingEnrollmentsDetails.Create(@event.EnrollmentId, @event.TrainingId, @event.Creator);
        await _enrollmentRepository.CreateAsync(trainingEnrollments, cancellationToken);
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }
    
    private async Task Handle(EnrollmentClosed @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var enrollment =
            await _enrollmentRepository.GetTrainingEnrollmentsWithoutUserEnrollmentsByIdAsync(@event.EnrollmentId, cancellationToken);
        enrollment?.TrainingEnrollmentClosed();
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(UserEnrollmentListCleared @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }
        var enrollment =
            await _enrollmentRepository.GetTrainingEnrollmentsDetailsByIdAsync(@event.EnrollmentId, cancellationToken);
        enrollment?.UserEnrollmentListAllCleared();
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(NewTrainingEnrollmentAdded @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }
        var enrollment =
            await _enrollmentRepository.GetTrainingEnrollmentsDetailsByIdAsync(@event.UserId, cancellationToken);
        enrollment?.TrainingEnrollmentAdded(@event.UserId);
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(UserEnrollmentAccepted @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }
        var enrollment =
            await _enrollmentRepository.GetTrainingEnrollmentsDetailsByIdAsync(@event.UserEnrollmentId,
                cancellationToken);
        enrollment?.NewUserEnrollmentAccepted(@event.UserEnrollmentId);
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }

    private async Task Handle(UserEnrollmentCancelled @event, CancellationToken cancellationToken = default)
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event));
        }

        var enrollment =
            await _enrollmentRepository.GetTrainingEnrollmentsDetailsByIdAsync(@event.UserEnrollmentId,
                cancellationToken);
        enrollment?.OneUserEnrollmentCancelled(@event.UserEnrollmentId);
        await _enrollmentRepository.SaveAsync(cancellationToken);
    }
}