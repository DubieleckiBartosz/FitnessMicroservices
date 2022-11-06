using Microsoft.EntityFrameworkCore;
using Training.API.Database;
using Training.API.Repositories.Interfaces;

namespace Training.API.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly DbSet<TrainingDetails> _trainings;
    private IQueryable<TrainingDetails> Details => GetDetails();
    public TrainingRepository(TrainingContext context)
    {
        _trainings = context?.Trainings ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateAsync(TrainingDetails training, CancellationToken cancellationToken = default)
    {
        await _trainings.AddAsync(training,cancellationToken);
    }

    public void Update(TrainingDetails training)
    {
        _trainings.Update(training);
    }

    public async Task<TrainingDetails> GetTrainingDetailsAsync(Guid trainingId, CancellationToken cancellationToken = default)
    {
        return await Details.FirstOrDefaultAsync(x => x.Id == trainingId, cancellationToken) ??
               throw new InvalidOperationException();
    }

    public async Task<TrainingDetails> GetTrainingsByStatusAsync(Guid trainingId, TrainingStatus status, CancellationToken cancellationToken = default)
    {
        return await Details.FirstOrDefaultAsync(x => x.Status == status, cancellationToken) ??
               throw new InvalidOperationException();
    }

    private IQueryable<TrainingDetails> GetDetails()
    {
        return _trainings.Include(_ => _.TrainingExercises)
            .Include(_ => _.TrainingUsers);
    }
}