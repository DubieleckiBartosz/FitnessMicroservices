using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using Training.API.Trainings.ReadModels;

namespace Training.API.Database.TypeConfigurations;

public class ExerciseTypeConfiguration : BaseTypeConfiguration<TrainingExercise>
{
    public override void Configure(EntityTypeBuilder<TrainingExercise> builder)
    {
        builder.ToTable("TrainingExercises");

        builder.HasKey(a => a.Id);

        base.Configure(builder);
    }
}
