using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Opinion.API.Domain;

namespace Opinion.API.Infrastructure.Database.ConfigurationTypes;

public class ReactionTypeConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.ToTable("Reactions");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.User).IsRequired(); 
        builder.Property(_ => _.ReactionFor).IsRequired(false);
        builder.Property(_ => _.ReactionType).IsRequired();

        builder.HasOne<Domain.Opinion>()
            .WithMany(_ => _.Reactions)
            .HasForeignKey(_ => _.OpinionId)
            .IsRequired(false);
    }
}