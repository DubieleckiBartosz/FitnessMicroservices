using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public class TrainingUser : IRead
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public int UserId { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public List<TrainingDetails> Trainings { get; private set; }

    internal TrainingUser()
    {
    }

    private TrainingUser(bool isDeleted, int userId, string email, string userName)
    {
        Id = Guid.NewGuid();
        IsDeleted = isDeleted;
        UserId = userId;
        Email = email;
        UserName = userName;
        Trainings = new List<TrainingDetails>();
    }

    public static TrainingUser Create(bool isDeleted, int userId, string email, string userName)
    {
        return new TrainingUser(isDeleted, userId, email, userName);
    }
}