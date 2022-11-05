using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public class TrainerInfo : IRead
{
    public Guid Id { get; private set; }
    public int UserId { get; private set; }
    public bool IsDeleted { get; set; }
    public int YearsExperience { get; private set; }
    public string TrainerName { get; private set; }

    internal TrainerInfo()
    {
    }

    private TrainerInfo(int yearsExperience, string trainerName, int userId)
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
        YearsExperience = yearsExperience;
        TrainerName = trainerName;
        UserId = userId;
    }

    public static TrainerInfo Create(int yearsExperience, string trainerName, int userId)
    {
        return new TrainerInfo(yearsExperience, trainerName, userId);
    }
}