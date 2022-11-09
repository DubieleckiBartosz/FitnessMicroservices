using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Fitness.Common.Core.Exceptions;
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

    public async Task<TrainingDetails> FindTrainingDetailsAsync(Guid trainingId, Guid? trainerCode, int? userId,
        CancellationToken cancellationToken = default)
    {
        //1. Training is available for enrolled users 
        //2. Creator always has access to the training
        //3. Everyone has access when the training is in 'PUBLIC' availability status

        Expression<Func<TrainingDetails, bool>> matchExpr = x =>
            (x.IsActive && x.Availability == TrainingAvailability.Public) ||
            (!x.IsActive && trainerCode != null && x.TrainerUniqueCode == trainerCode) ||
            (((x.IsActive && x.Availability == TrainingAvailability.Group) ||
              (x.Availability == TrainingAvailability.Private && userId != null)) &&
             x.TrainingUsers.Any(y => y.UserId == userId));

        return await Details.Where(matchExpr).FirstOrDefaultAsync(x => x.Id == trainingId, cancellationToken) ??
               throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
    }

    public async Task<TrainingDetails> GetTrainingDetailsByIdAsync(Guid trainingId, CancellationToken cancellationToken = default)
    {
        return await Details.FirstOrDefaultAsync(x => x.Id == trainingId, cancellationToken) ??
               throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
    }

    public async Task<TrainingDetails> GetTrainingsByStatusAsync(Guid trainingId, TrainingStatus status, CancellationToken cancellationToken = default)
    {
        return await Details.FirstOrDefaultAsync(x => x.Status == status, cancellationToken) ??
               throw new NotFoundException(Strings.TrainingNotFoundMessage, Strings.TrainingNotFoundTitle);
    }

    private IQueryable<TrainingDetails> GetDetails()
    {
        return _trainings.Include(_ => _.TrainingExercises)
            .Include(_ => _.TrainingUsers);
    }
}