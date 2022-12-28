using Exercise.Application.Contracts;
using Exercise.Domain.ValueObjects;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.FileOperations;
using MediatR;

namespace Exercise.Application.Features.ExerciseFeatures.Commands.AddNewImageToExercise;

public class AddNewImageToExerciseHandler : ICommandHandler<AddNewImageToExerciseCommand, Guid>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IFileService _fileService;

    public AddNewImageToExerciseHandler(IExerciseRepository exerciseRepository, IFileService fileService)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }
    public async Task<Guid> Handle(AddNewImageToExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exercise == null)
        {
            throw new NotFoundException("Exercise cannot be null.", "Exercise not found");
        }

        var finalPath = request.Path.CreatePath(exercise.Name, request.Image.FileName);


        var description = ImageDescription.Create(request.Description);
        var image = exercise.AddNewImage(finalPath, request.Title, request.IsMain, description);

        await _exerciseRepository.AddNewImageAsync(image);
        await _fileService.SaveFileAsync(request.Image, request.Path, exercise.Name);

        return image.Id;
    }
}