using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.API.Trainings.ReadModels;

namespace Training.API.Database.TypeConfigurations
{
    public class TrainerTypeConfiguration : BaseTypeConfiguration<TrainerInfo>
    {
        public override void Configure(EntityTypeBuilder<TrainerInfo> builder)
        {
            builder.ToTable("TrainerInfos");

            builder.HasKey(a => a.Id);

            base.Configure(builder);
        }
    }
}
