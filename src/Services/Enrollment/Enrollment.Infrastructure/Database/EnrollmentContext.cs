using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Infrastructure.Database;

public class EnrollmentContext : DbContext
{
    public DbSet<TrainingEnrollmentsDetails> Enrollments { get; set; }
    public DbSet<UserEnrollment> UserEnrollments { get; set; }

    public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEnrollmentRead && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((IEnrollmentRead)entityEntry.Entity).Modified = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((IEnrollmentRead)entityEntry.Entity).Created = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
     
}