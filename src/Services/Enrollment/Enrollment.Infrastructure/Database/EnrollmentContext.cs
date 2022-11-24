using Enrollment.Application.Enrollments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Enrollment.Infrastructure.Database;

public class EnrollmentContext : DbContext
{
    public DbSet<Application.Enrollments.Enrollment> Enrollments { get; set; }
    public DbSet<UserEnrollment> UserEnrollments { get; set; }

    public EnrollmentContext(DbContextOptions<EnrollmentContext> options) : base(options)
    {
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.EnableDetailedErrors()
    //        .ConfigureWarnings(
    //            b => b.Log(
    //                (RelationalEventId.ConnectionOpened, LogLevel.Information),
    //                (RelationalEventId.ConnectionClosed, LogLevel.Information)));
}