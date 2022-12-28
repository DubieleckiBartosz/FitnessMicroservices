using Exercise.Domain.Entities;

namespace Exercise.Application.Contracts;

public interface IExerciseRepository
{
    Task<Domain.Entities.Exercise?> GetByIdAsync(Guid exerciseId);
    Task<List<Domain.Entities.Exercise>?> GetBySearchAsync(Guid? id, string? name, string sortModelType,
        string sortModelName, int pageNumber, int pageSize);

    Task<Domain.Entities.Exercise?> GetByNameAsync(string name);
    Task AddAsync(Domain.Entities.Exercise exercise);
    Task UpdateAsync(Domain.Entities.Exercise exercise);
    Task AddNewImageAsync(ExerciseImage exerciseImage);
}