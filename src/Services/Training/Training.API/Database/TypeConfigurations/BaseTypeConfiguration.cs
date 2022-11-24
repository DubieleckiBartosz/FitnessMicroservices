using Fitness.Common.Projection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Training.API.Database.TypeConfigurations
{
    public class BaseTypeConfiguration<TReadModel> : IEntityTypeConfiguration<TReadModel> where TReadModel : class, ITrainingRead
    {
        public virtual void Configure(EntityTypeBuilder<TReadModel> builder)
        {
            builder.HasQueryFilter(_ => _.IsDeleted == false);
        }
    }
}
