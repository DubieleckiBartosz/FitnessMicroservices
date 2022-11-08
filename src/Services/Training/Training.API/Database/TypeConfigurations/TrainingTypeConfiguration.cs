using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Training.API.Database.TypeConfigurations;

public class TrainingTypeConfiguration : BaseTypeConfiguration<TrainingDetails>
{
    public override void Configure(EntityTypeBuilder<TrainingDetails> builder)
    {
        builder.ToTable("Trainings");

        builder.HasKey(_ => _.Id);

        builder.OwnsMany(_ => _.TrainingExercises, x =>
        {
            x.HasKey(_ => _.Id);
            x.Property(_ => _.Id).ValueGeneratedNever();
            x.ToTable("TrainingExercises");
        });

        builder.HasMany(_ => _.TrainingUsers).WithMany(_ => _.Trainings);
        builder.Property(_ => _.TrainerUniqueCode).IsRequired();

        builder.Property(_ => _.Price).IsRequired(false);
        builder.Property(_ => _.DurationTrainingInMinutes).IsRequired(false);
        builder.Property(_ => _.BreakBetweenExercisesInMinutes).IsRequired(false);

        base.Configure(builder);
    }
}