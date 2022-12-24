using Enrollment.Application.Constants;
using Enrollment.Application.Exceptions; 

namespace Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

public class UserEnrollment : IEnrollmentRead
{
    public Guid Id { get; private set; }
    public Guid EnrollmentId { get; private set; }
    public int UserId { get; private set; }
    public bool Cancelled { get; private set; }
    public DateTime? CancelledDate { get; private set; }
    public bool Accepted { get; private set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    private UserEnrollment(Guid enrollmentId, int userId)
    {
        Id = Guid.NewGuid();
        EnrollmentId = enrollmentId;
        UserId = userId;
        Accepted = false;
        Cancelled = false;
        CancelledDate = null;
    }

    public static UserEnrollment Create(Guid enrollmentId, int userId)
    {
        return new UserEnrollment(enrollmentId, userId);
    }

    public void Cancel()
    {
        Cancelled = true;
        CancelledDate = DateTime.UtcNow;
    }
    
    public void AcceptEnrollment()
    {
        if (Cancelled)
        {
            throw new EnrollmentServiceBusinessException(Strings.UserEnrollmentIsCancelledTitle,
                Strings.UserEnrollmentIsCancelledMessage);
        }

        Accepted = true;
    } 
}