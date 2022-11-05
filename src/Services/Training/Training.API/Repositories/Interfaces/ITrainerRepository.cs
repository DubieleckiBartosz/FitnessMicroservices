using Training.API.Trainings.ReadModels;

namespace Training.API.Repositories.Interfaces;

public interface ITrainerRepository
{
    Task<TrainerInfo?> GetTrainerByUserIdAsync(int userId);
    Task AddNewTrainerInfoAsync(TrainerInfo newTrainerInfo);
}