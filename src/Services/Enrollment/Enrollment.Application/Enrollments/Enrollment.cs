using Enrollment.Application.Constants;
using Enrollment.Application.Enrollments.AcceptingUserEnrollment;
using Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;
using Enrollment.Application.Enrollments.CancellationUserEnrollment;
using Enrollment.Application.Enrollments.ClearingUserEnrollmentList;
using Enrollment.Application.Enrollments.ClosingEnrollment;
using Enrollment.Application.Enrollments.Enums;
using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Enrollment.Application.Enrollments.StartingTrainingEnrollments;
using Enrollment.Application.Exceptions;
using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments;

public class Enrollment : Aggregate
{
    public Guid Creator { get; private set; }
    public Guid TrainingId { get; private set; }
    public Status CurrentStatus { get; private set; }
    public List<UserEnrollment>? UserEnrollments { get; private set; }

    private Enrollment(Guid trainingId, Guid creator)
    {
        var @event = TrainingEnrollmentsStarted.Create(Guid.NewGuid(), trainingId, creator);
        Apply(@event);
        Enqueue(@event);
    }

    public static Enrollment Create(Guid trainingId, Guid creator)
    {
        return new Enrollment(trainingId, creator);
    }
    
    public void AcceptUserEnrollment(Guid userEnrollmentId, Guid acceptBy)
    {
        if (acceptBy != Creator)
        {
            throw new EnrollmentServiceBusinessException(Strings.NoPermissionsTitle,
                Strings.NoPermissionsMessage);
        }

        StatusMustBeOpen();

        var enrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == userEnrollmentId);
        if (enrollment == null)
        {
            throw new EnrollmentServiceBusinessException(Strings.UserEnrollmentNotFoundTitle,
                Strings.UserEnrollmentNotFoundMessage);
        }
         
        var @event = UserEnrollmentAccepted.Create(userEnrollmentId, acceptBy);
        Apply(@event);
        Enqueue(@event);
    } 

    public void Close(Guid closeBy)
    {
        if (closeBy != Creator)
        {
            throw new EnrollmentServiceBusinessException(Strings.NoPermissionsTitle,
                Strings.NoPermissionsMessage);
        }

        if (CurrentStatus != Status.Open || CurrentStatus != Status.Suspended)
        {
            throw new EnrollmentServiceBusinessException(Strings.InvalidStatusTitle,
                Strings.InvalidStatusMessage);
        }
         
        var @event = EnrollmentClosed.Create(this.Id);
        Apply(@event);
        Enqueue(@event);
    }

    public void ClearUserEnrollmentList(Guid cleanBy)
    { 
        if (cleanBy != Creator)
        {
            throw new EnrollmentServiceBusinessException(Strings.NoPermissionsTitle,
                Strings.NoPermissionsMessage);
        }
         
        var @event = UserEnrollmentListCleared.Create(this.Id);
        Apply(@event);
        Enqueue(@event);
    }

    public void CancelUserEnrollment(Guid userEnrollmentId, Guid cancelBy)
    {
        var userEnrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == userEnrollmentId);
        if (userEnrollment == null)
        {
            throw new EnrollmentServiceBusinessException(Strings.UserEnrollmentNotFoundTitle,
                Strings.UserEnrollmentNotFoundMessage);
        }

        if (userEnrollment.UserId == cancelBy)
        {
            StatusMustBeOpen(); 
        }

        if (userEnrollment.UserId != cancelBy && cancelBy != Creator)
        {
            throw new EnrollmentServiceBusinessException(Strings.NoPermissionsTitle,
                Strings.NoPermissionsMessage);
        }
         
        var @event = UserEnrollmentCancelled.Create(userEnrollmentId, cancelBy);
        Apply(@event);
        Enqueue(@event);
    }

    public void NewUserTrainingEnrollment(Guid userId)
    {
        StatusMustBeOpen();

         var alreadyExists = UserEnrollments?.Any(_ => _.UserId == userId) ?? false;
        if (alreadyExists)
        {
            throw new EnrollmentServiceBusinessException(Strings.UserHasEnrollmentTitle,
                Strings.UserHasEnrollmentMessage);
        }

        var @event = NewTrainingEnrollmentAdded.Create(userId, this.TrainingId);
        Apply(@event);
        Enqueue(@event);
    }

    protected override void When(IEvent @event)
    {
        switch (@event)
        {
            case TrainingEnrollmentsStarted e:
                StartedEnrollments(e);
                break;
            case NewTrainingEnrollmentAdded e:
                TrainingEnrollmentAdded(e);
                break;
            case UserEnrollmentAccepted e:
                NewUserEnrollmentAccepted(e);
                break;
            case UserEnrollmentListCleared e:
                UserEnrollmentListAllCleared(e);
                break;
            case UserEnrollmentCancelled e:
                OneUserEnrollmentCancelled(e);
                break;
            case EnrollmentClosed e:
                TrainingEnrollmentClosed(e);
                break;
        }
    }

    public void StartedEnrollments(TrainingEnrollmentsStarted @event)
    {
        Id = @event.EnrollmentId;
        Creator = @event.Creator;
        TrainingId = @event.TrainingId;
        CurrentStatus = Status.Open;
    }
    public void TrainingEnrollmentAdded(NewTrainingEnrollmentAdded @event)
    { 
        var newUserTrainingEnrollment = UserEnrollment.Create(Id, @event.UserId);
        UserEnrollments ??= new List<UserEnrollment>();
        UserEnrollments.Add(newUserTrainingEnrollment);
    }
    
    public void NewUserEnrollmentAccepted(UserEnrollmentAccepted @event)
    {
        var enrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == @event.UserEnrollmentId);
        enrollment?.AcceptEnrollment();
    }
    public void UserEnrollmentListAllCleared(UserEnrollmentListCleared @event)
    {
        UserEnrollments?.Clear();
    }

    public void OneUserEnrollmentCancelled(UserEnrollmentCancelled @event)
    {
        var userEnrollment = UserEnrollments?.FirstOrDefault(_ => _.Id == @event.UserEnrollmentId); 
        userEnrollment?.Cancel();
    }

    public void TrainingEnrollmentClosed(EnrollmentClosed @event)
    {
        CurrentStatus = Status.Closed;
    }

    private void StatusMustBeOpen()
    {
        if (CurrentStatus != Status.Open)
        {
            throw new EnrollmentServiceBusinessException(Strings.EnrollmentsMustBeOpenTitle,
                Strings.EnrollmentsMustBeOpenMessage);
        }
    }  
}