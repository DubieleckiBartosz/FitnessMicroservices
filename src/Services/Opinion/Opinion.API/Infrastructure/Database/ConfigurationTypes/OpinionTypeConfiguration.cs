using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Opinion.API.Infrastructure.Database.ConfigurationTypes;

public class OpinionTypeConfiguration : IEntityTypeConfiguration<Domain.Opinion>
{
    public void Configure(EntityTypeBuilder<Domain.Opinion> builder)
    {
        builder.ToTable("Opinions");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Comment).IsRequired();
        builder.Property(_ => _.Creator).IsRequired();
        builder.Property(_ => _.OpinionFor).IsRequired();

        builder.HasMany(_ => _.Reactions).WithOne().IsRequired(false);
    }
}