using Microsoft.EntityFrameworkCore;
using Training.API.Database;
using Training.API.Repositories.Interfaces;
using Training.API.Trainings.ReadModels;

namespace Training.API.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly DbSet<TrainerInfo> _trainers;

    public TrainerRepository(TrainingContext context)
    {
        _trainers = context?.TrainerInfos ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TrainerInfo?> GetTrainerByUserIdAsync(int userId)
    {
        return await _trainers.FirstOrDefaultAsync(_ => _.UserId == userId);
    }

    public async Task AddNewTrainerInfoAsync(TrainerInfo newTrainerInfo)
    {
        await _trainers.AddAsync(newTrainerInfo);
    }
}