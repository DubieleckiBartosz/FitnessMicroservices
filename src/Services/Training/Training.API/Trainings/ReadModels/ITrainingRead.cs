using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public interface ITrainingRead : IRead
{
    public bool IsDeleted { get; set; }
}