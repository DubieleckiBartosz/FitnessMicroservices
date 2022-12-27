namespace Exercise.Application.Contracts;

public interface IExerciseRepository
{
    Task<Domain.Entities.Exercise> GetByIdAsync(Guid exerciseId);
    Task<List<Domain.Entities.Exercise>> GetBySearchAsync(); 
    Task AddAsync(Domain.Entities.Exercise exercise);
    Task UpdateAsync(Domain.Entities.Exercise exercise);
    Task AddNewImageAsync(Domain.Entities.Exercise exercise);
}