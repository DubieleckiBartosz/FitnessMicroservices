namespace Training.API.Repositories.Interfaces;

public interface IWrapperRepository
{ 
    ITrainingRepository TrainingRepository { get; }
    Task SaveAsync(CancellationToken cancellationToken = default);
    void Save();
}