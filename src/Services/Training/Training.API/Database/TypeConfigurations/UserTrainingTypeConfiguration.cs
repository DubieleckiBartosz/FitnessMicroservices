using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Training.API.Database.TypeConfigurations
{
    public class UserTrainingTypeConfiguration : BaseTypeConfiguration<TrainingUser>
    {
        public override void Configure(EntityTypeBuilder<TrainingUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(a => a.Id);
            builder.Property(_ => _.Id).ValueGeneratedNever();

            builder.Property(_ => _.UserId).IsRequired();

            base.Configure(builder);
        }
    }
}
