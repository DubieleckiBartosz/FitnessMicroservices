using Training.API.Handlers.ViewModels;

namespace Training.API.Queries.TrainingQueries;

public record GetTrainingByIdQuery(Guid TrainingId) : IQuery<TrainingDetailsViewModel>
{
    public static GetTrainingByIdQuery Create(Guid trainingId)
    {
        return new GetTrainingByIdQuery(trainingId);
    }
}