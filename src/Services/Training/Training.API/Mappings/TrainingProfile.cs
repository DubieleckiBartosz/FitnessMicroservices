using AutoMapper;
using Training.API.Handlers.ViewModels;

namespace Training.API.Mappings;

public class TrainingProfile : Profile
{
    public TrainingProfile()
    {
        this.CreateMap<TrainingDetails, TrainingDetailsViewModel>();
        this.CreateMap<TrainingExercise, ExerciseViewModel>();
    }
}