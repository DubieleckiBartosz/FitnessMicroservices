using Fitness.Common.Projection;

namespace Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

public interface IEnrollmentRead : IRead
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}