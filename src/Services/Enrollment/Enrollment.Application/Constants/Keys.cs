namespace Enrollment.Application.Constants;

public static class Keys
{
    public const string ShareTrainingQueueRoutingKey = "training_shared_key";
    public const string CloseTrainingQueueRoutingKey = "training_marked_as_historical_key";
    public const string AvailabilityChangedQueueRoutingKey = "availability_changed_key";
}