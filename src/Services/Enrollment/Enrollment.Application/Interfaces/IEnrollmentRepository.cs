using Enrollment.Application.Enrollments;
using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

namespace Enrollment.Application.Interfaces;

public interface IEnrollmentRepository
{
    Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsWithoutUserEnrollmentsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<UserEnrollment>> GetUserEnrollmentByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task CreateAsync(TrainingEnrollmentsDetails trainingEnrollmentsDetails, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}