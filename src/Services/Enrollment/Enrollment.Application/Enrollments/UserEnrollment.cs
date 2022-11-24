namespace Enrollment.Application.Enrollments;

public class UserEnrollment
{
    public Guid TrainingId { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? InActiveTime { get; private set; }

    public UserEnrollment(Guid trainingId, Guid userId)
    {
        TrainingId = trainingId;
        UserId = userId;
        IsActive = true;
        InActiveTime = null;
    }

    public void Inactive()
    {
        IsActive = false;
        InActiveTime = DateTime.UtcNow;
    }
}