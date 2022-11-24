using Enrollment.Application.Enrollments.Enums;
using Enrollment.Application.Enrollments.StartingTrainingEnrollments;
using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events; 

namespace Enrollment.Application.Enrollments;

public class Enrollment : Aggregate
{
    public Guid TrainingId { get; private set; }
    public Status CurrentStatus { get; private set; }
    public List<UserEnrollment>? UserEnrollments { get; private set; }
    private Enrollment(Guid trainingId)
    { 
        var @event = TrainingEnrollmentsStarted.Create(trainingId);
        this.Apply(@event);
        this.Enqueue(@event);
    }

    public static Enrollment Create(Guid trainingId)
    { 
        return new Enrollment(trainingId);
    }

    protected override void When(IEvent @event)
    {
        switch (@event)
        {
            case TrainingEnrollmentsStarted e:
                StartedEnrollments(e);
                break; 
            default:
                break;
        }
    }

    public void StartedEnrollments(TrainingEnrollmentsStarted @event)
    {
        TrainingId = @event.TrainingId;
        CurrentStatus = Status.Open;
        UserEnrollments = new List<UserEnrollment>();
    }
}