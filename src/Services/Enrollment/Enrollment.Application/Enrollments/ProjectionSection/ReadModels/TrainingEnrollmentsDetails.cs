using Enrollment.Application.Enrollments.Enums;
using Fitness.Common.Projection;

namespace Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

public class TrainingEnrollmentsDetails : IRead
{
    public Guid Id { get; } 
    public Status CurrentStatus { get; }
    public List<UserEnrollment>? UserEnrollments { get; }

    private TrainingEnrollmentsDetails(Guid trainingId)
    {
        Id = trainingId;
        CurrentStatus = Status.Open;
        UserEnrollments = new List<UserEnrollment>();
    }

    public static TrainingEnrollmentsDetails Create(Guid trainingId)
    {
        return new TrainingEnrollmentsDetails(trainingId);
    }
}