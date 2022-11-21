using Fitness.Common.EventStore.Events;
using Fitness.Common.EventStore.Repository;

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

public class StartingEnrollments //: IEventHandler<TrainingEnrollmentsStarted>
{
    private readonly IRepository<Enrollment> _repository;

    public StartingEnrollments(IRepository<Enrollment> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task Handle(TrainingEnrollmentsStarted notification, CancellationToken cancellationToken)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }
         
    }
}