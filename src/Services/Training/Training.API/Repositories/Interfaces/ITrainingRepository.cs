using Training.API.Trainings.Enums;
using Training.API.Trainings.ReadModels;

namespace Training.API.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task<TrainingDetails> GetTrainingDetails(Guid trainingId);
        Task<TrainingDetails> GetTrainingsByStatus(Guid trainingId, TrainingStatus status);

    }
}
