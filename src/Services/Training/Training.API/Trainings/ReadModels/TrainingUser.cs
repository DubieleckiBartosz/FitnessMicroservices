using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public class TrainingUser : IRead
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public int UserId { get; private set; }
    public List<TrainingDetails> Trainings { get; private set; }

    internal TrainingUser()
    {
    }

    private TrainingUser(int userId)
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
        UserId = userId;
        Trainings = new List<TrainingDetails>();
    }

    public static TrainingUser Create(int userId)
    {
        return new TrainingUser(userId);
    }
}