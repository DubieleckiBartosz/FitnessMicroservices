using Exercise.Application.Features.Views;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Queries.GetExerciseById;

public record GetExerciseByIdQuery(Guid ExerciseId) : IQuery<GetExerciseByIdViewModel>
{
    public static GetExerciseByIdQuery Create(Guid exerciseId)
    {
        return new GetExerciseByIdQuery(exerciseId);
    }
}