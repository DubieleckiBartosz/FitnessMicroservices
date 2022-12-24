using Enrollment.Application.Enrollments.Enums;

namespace Enrollment.Application.Enrollments.ProjectionSection.ReadModels;

public class TrainingEnrollmentsDetails : IEnrollmentRead
{
    public Guid Id { get; private set; }
    public Guid Creator { get; private set; }
    public Guid TrainingId { get; private set; }
    public Status CurrentStatus { get; private set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public List<UserEnrollment>? UserEnrollments { get; private set; }

    public TrainingEnrollmentsDetails()
    {
    }
    private TrainingEnrollmentsDetails(Guid enrollmentId, Guid trainingId, Guid creator)
    {
        Id = enrollmentId;
        Creator = trainingId;
        TrainingId = creator;
        CurrentStatus = Status.Open;
    }

    public static TrainingEnrollmentsDetails Create(Guid enrollmentId, Guid trainingId, Guid creator)
    {
        return new TrainingEnrollmentsDetails(enrollmentId, trainingId, creator);
    }

    public void TrainingEnrollmentAdded(int userId)
    {
        var newUserTrainingEnrollment = UserEnrollment.Create(Id, userId);
        UserEnrollments ??= new List<UserEnrollment>();
        UserEnrollments.Add(newUserTrainingEnrollment);
    }

    public void NewUserEnrollmentAccepted(Guid userEnrollmentId)
    {
        var enrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == userEnrollmentId);
        enrollment?.AcceptEnrollment();
    }
    public void UserEnrollmentListAllCleared()
    {
        UserEnrollments?.Clear();
    }

    public void OneUserEnrollmentCancelled(Guid userEnrollmentId)
    {
        var userEnrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == userEnrollmentId);
        userEnrollment?.Cancel();
    }

    public void TrainingEnrollmentClosed()
    {
        CurrentStatus = Status.Closed;
    }
    public void Open()
    {
        CurrentStatus = Status.Open;
    }
}