namespace Training.API.Repositories.Interfaces
{
    public interface ITrainingRepository
    {
        Task CreateAsync(TrainingDetails training, CancellationToken cancellationToken = default);
        void Update(TrainingDetails training);

        Task<TrainingDetails> FindTrainingDetailsAsync(Guid trainingId, Guid? trainerCode, int? userId,
            CancellationToken cancellationToken = default);
        Task<TrainingDetails> GetTrainingDetailsByIdAsync(Guid trainingId, CancellationToken cancellationToken = default);
        Task<TrainingDetails> GetTrainingsByStatusAsync(Guid trainingId, TrainingStatus status, CancellationToken cancellationToken = default);
    }
}
