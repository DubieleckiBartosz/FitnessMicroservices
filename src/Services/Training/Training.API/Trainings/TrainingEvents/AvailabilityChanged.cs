namespace Training.API.Trainings.TrainingEvents;

public record AvailabilityChanged(Guid TrainingId, TrainingAvailability NewAvailability) : IEvent
{
    public static AvailabilityChanged Create(Guid trainingId, TrainingAvailability newAvailability)
    {
        return new AvailabilityChanged(trainingId, newAvailability);
    }
}