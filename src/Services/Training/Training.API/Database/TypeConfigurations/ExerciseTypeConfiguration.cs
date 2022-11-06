//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Training.API.Database.TypeConfigurations;

//public class ExerciseTypeConfiguration : BaseTypeConfiguration<TrainingExercise>
//{
//    public override void Configure(EntityTypeBuilder<TrainingExercise> builder)
//    {
//        builder.ToTable("TrainingExercises");

//        builder.HasKey(_ => _.Id);
//        builder.Property(_ => _.Id).ValueGeneratedNever();

//        base.Configure(builder);
//    }
//}
