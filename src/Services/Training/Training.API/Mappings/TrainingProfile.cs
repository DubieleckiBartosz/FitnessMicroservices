using AutoMapper;
using Training.API.Handlers.ViewModels;

namespace Training.API.Mappings;

public class TrainingProfile : Profile
{
    public TrainingProfile()
    {
        CreateMap<TrainingDetails, TrainingDetailsViewModel>()
            .ForMember(dest => dest.NumberUsersEnrolled, opt => opt.MapFrom(src => src.GetUsersEnrolledCount()));

        CreateMap<TrainingExercise, ExerciseViewModel>();
    }
}