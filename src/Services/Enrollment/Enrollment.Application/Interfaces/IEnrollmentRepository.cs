using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

namespace Enrollment.Application.Interfaces;

public interface IEnrollmentRepository
{
    Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsDetailsByTrainingIdAsync(Guid trainingId, CancellationToken cancellationToken);
    Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsWithoutUserEnrollmentsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<UserEnrollment>> GetUserEnrollmentByUserAsync(int userId, CancellationToken cancellationToken);
    Task CreateAsync(TrainingEnrollmentsDetails trainingEnrollmentsDetails, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}