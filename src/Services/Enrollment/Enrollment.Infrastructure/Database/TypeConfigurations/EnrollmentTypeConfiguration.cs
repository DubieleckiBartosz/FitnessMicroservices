using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrollment.Infrastructure.Database.TypeConfigurations;

public class EnrollmentTypeConfiguration : IEntityTypeConfiguration<TrainingEnrollmentsDetails>
{
    public void Configure(EntityTypeBuilder<TrainingEnrollmentsDetails> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.CurrentStatus)
            .HasConversion<int>();

        builder.HasIndex(_ => _.TrainingId).IsUnique(); 
        builder.Property(_ => _.TrainingId).IsRequired();
        builder.HasMany(_ => _.UserEnrollments).WithOne().HasForeignKey(_ => _.EnrollmentId); 
    }
}