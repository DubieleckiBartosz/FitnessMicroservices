using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels
{
    public class TrainingUser : IRead
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
