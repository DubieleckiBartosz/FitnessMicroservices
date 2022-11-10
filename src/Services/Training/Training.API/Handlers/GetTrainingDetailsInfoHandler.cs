using AutoMapper;
using Fitness.Common.CommonServices;
using Fitness.Common.Extensions;
using Training.API.Handlers.ViewModels;
using Training.API.Queries.TrainingQueries;
using Training.API.Repositories.Interfaces;

namespace Training.API.Handlers;

public class GetTrainingDetailsInfoHandler : IQueryHandler<GetTrainingByIdQuery, TrainingDetailsViewModel>
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;

    public GetTrainingDetailsInfoHandler(IWrapperRepository wrapperRepository, ICurrentUser currentUser, IMapper mapper)
    {
        _trainingRepository = wrapperRepository.TrainingRepository ?? throw new ArgumentNullException(nameof(wrapperRepository));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TrainingDetailsViewModel> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.UserId;
        var trainerCode = _currentUser.TrainerCode?.ParseToGuidOrThrow();

        var training = await _trainingRepository.FindTrainingDetailsAsync(request.TrainingId, trainerCode, currentUserId, cancellationToken);

        return _mapper.Map<TrainingDetailsViewModel>(training);
    }
}