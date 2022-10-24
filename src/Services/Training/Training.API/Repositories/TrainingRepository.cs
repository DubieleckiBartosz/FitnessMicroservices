using Microsoft.EntityFrameworkCore;
using Training.API.Database;
using Training.API.Repositories.Interfaces;
using Training.API.Trainings.Enums;
using Training.API.Trainings.ReadModels;

namespace Training.API.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly DbSet<TrainingDetails> _trainings;
    private IQueryable<TrainingDetails> Details => GetDetails();
    public TrainingRepository(TrainingContext context)
    {
        _trainings = context?.Trainings ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TrainingDetails> GetTrainingDetails(Guid trainingId)
    {
        return await Details.Where(x => x.TrainingId == trainingId).FirstOrDefaultAsync() ??
               throw new InvalidOperationException();
    }

    public async Task<TrainingDetails> GetTrainingsByStatus(Guid trainingId, TrainingStatus status)
    {
        return await Details.Where(x => x.Status == status).FirstOrDefaultAsync() ??
               throw new InvalidOperationException();
    }

    private IQueryable<TrainingDetails> GetDetails()
    {
        return _trainings.AsNoTracking()
            .Include(_ => _.TrainingExercises)
            .Include(_ => _.TrainingUsers);
    }
}