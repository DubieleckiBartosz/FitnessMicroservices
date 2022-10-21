using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels
{
    public class TrainerInfo : IRead
    {
        public Guid Id { get; set; }
        public int YearsExperience { get; set; }
        public string TrainerName { get; set; }
    }
}
