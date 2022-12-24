using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrollment.Infrastructure.Database.TypeConfigurations;

public class UserEnrollmentTypeConfiguration : IEntityTypeConfiguration<UserEnrollment>
{
    public void Configure(EntityTypeBuilder<UserEnrollment> builder)
    { 
        builder.HasKey(a => a.Id);
        builder.Property(_ => _.Id).ValueGeneratedNever();

        builder.HasOne<TrainingEnrollmentsDetails>().WithMany(_ => _.UserEnrollments)
            .HasForeignKey(_ => _.EnrollmentId);
    }
}