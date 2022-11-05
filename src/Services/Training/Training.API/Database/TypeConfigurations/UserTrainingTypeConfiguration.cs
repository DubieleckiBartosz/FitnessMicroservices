using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.API.Trainings.ReadModels;

namespace Training.API.Database.TypeConfigurations
{
    public class UserTrainingTypeConfiguration : BaseTypeConfiguration<TrainingUser>
    {
        public override void Configure(EntityTypeBuilder<TrainingUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(a => a.Id);

            builder.Property(_ => _.UserId).IsRequired();
            builder.Property(_ => _.Email).IsRequired();
            builder.Property(_ => _.UserName).HasMaxLength(30).IsRequired();

            base.Configure(builder);
        }
    }
}
