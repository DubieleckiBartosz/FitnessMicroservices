using Microsoft.EntityFrameworkCore;
using Training.API.Trainings;
using Training.API.Trainings.ReadModels;

namespace Training.API.Database
{
    public class TrainingContext : DbContext
    {
        public DbSet<TrainingDetails> Trainings { get; set; }
        public DbSet<TrainingUser> TrainingUsers { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
        {

        }
    }
}
