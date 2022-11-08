using Fitness.Common.CommonServices;
using Training.API.Handlers.ViewModels;
using Training.API.Queries.TrainingQueries;
using Training.API.Repositories.Interfaces;

namespace Training.API.Handlers;

public class GetTrainingDetailsInfoHandler : IQueryHandler<GetTrainingByIdQuery, TrainingDetailsViewModel>
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly ICurrentUser _currentUser;

    public GetTrainingDetailsInfoHandler(IWrapperRepository wrapperRepository, ICurrentUser currentUser)
    {
        _trainingRepository = wrapperRepository.TrainingRepository ?? throw new ArgumentNullException(nameof(wrapperRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    public async Task<TrainingDetailsViewModel> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}