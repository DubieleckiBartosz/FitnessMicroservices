using Training.API.Trainings.Enums;
using Training.API.Trainings.ReadModels;

namespace Training.API.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task CreateAsync(TrainingDetails training, CancellationToken cancellationToken = default);
        void Update(TrainingDetails training);
        Task<TrainingDetails> GetTrainingDetailsAsync(Guid trainingId, CancellationToken cancellationToken = default);
        Task<TrainingDetails> GetTrainingsByStatusAsync(Guid trainingId, TrainingStatus status, CancellationToken cancellationToken = default);
    }
}
