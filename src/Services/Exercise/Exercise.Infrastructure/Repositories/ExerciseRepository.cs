using Exercise.Application.Contracts;
using Exercise.Application.Options;
using Fitness.Common.Logging;
using Microsoft.Extensions.Options;

namespace Exercise.Infrastructure.Repositories;

public class ExerciseRepository : BaseRepository<ExerciseRepository>, IExerciseRepository
{
    public ExerciseRepository(IOptions<DatabaseConnection> dbConnection,
        ILoggerManager<ExerciseRepository> loggerManager) : base(dbConnection, loggerManager)
    {
    }

    public Task<Domain.Entities.Exercise> GetByIdAsync(Guid exerciseId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Entities.Exercise>> GetBySearchAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Domain.Entities.Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entities.Exercise exercise)
    {
        throw new NotImplementedException();
    }

    public Task AddNewImageAsync(Domain.Entities.Exercise exercise)
    {
        throw new NotImplementedException();
    } 
}