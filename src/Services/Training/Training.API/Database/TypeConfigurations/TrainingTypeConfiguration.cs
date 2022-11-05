using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.API.Trainings.ReadModels;

namespace Training.API.Database.TypeConfigurations
{
    public class TrainingTypeConfiguration : BaseTypeConfiguration<TrainingDetails>
    {
        public override void Configure(EntityTypeBuilder<TrainingDetails> builder)
        {
            builder.ToTable("Trainings");

            builder.HasKey(a => a.Id);

            builder.HasMany(_ => _.TrainingExercises).WithOne();
            builder.HasMany(_ => _.TrainingUsers).WithMany(_ => _.Trainings);
            builder.HasOne<TrainerInfo>().WithMany().HasForeignKey(_ => _.CreatorId).IsRequired();

            builder.Property(_ => _.Price).IsRequired(false);
            builder.Property(_ => _.DurationTrainingInMinutes).IsRequired(false);
            builder.Property(_ => _.BreakBetweenExercisesInMinutes).IsRequired(false);

            base.Configure(builder);
        }
    }
}
