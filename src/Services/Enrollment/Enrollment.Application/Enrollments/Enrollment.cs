using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments;

public class Enrollment : Aggregate
{
    protected override void When(IEvent @event)
    {
        throw new NotImplementedException();
    }
}