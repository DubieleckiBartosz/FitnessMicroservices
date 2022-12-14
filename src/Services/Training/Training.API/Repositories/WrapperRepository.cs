using Training.API.Database;
using Training.API.Repositories.Interfaces;

namespace Training.API.Repositories;

public class WrapperRepository : IWrapperRepository
{
    private ITrainingRepository _trainingRepository; 
    private readonly TrainingContext _context;

    public WrapperRepository(TrainingContext context)
    {
        _context = context;
    } 
    public ITrainingRepository TrainingRepository
    {
        get { return _trainingRepository ??= new TrainingRepository(_context); }
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}