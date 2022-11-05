namespace Training.API.Repositories.Interfaces;

public interface IWrapperRepository
{
    ITrainerRepository TrainerRepository { get; }
    ITrainingRepository TrainingRepository { get; }
    Task SaveAsync(CancellationToken cancellationToken = default);
    void Save();
}