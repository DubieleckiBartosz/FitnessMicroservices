using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseVideoLink;

public record UpdateExerciseVideoLinkCommand(Guid ExerciseId, string Link) : ICommand<Unit>
{
    public static UpdateExerciseVideoLinkCommand Create(UpdateExerciseVideoLinkParameters parameters)
    {
        return new UpdateExerciseVideoLinkCommand(parameters.ExerciseId, parameters.Link);
    }
}