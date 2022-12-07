using Enrollment.Application.Enrollments;
using Enrollment.Application.Enrollments.ProjectionSection.ReadModels;
using Enrollment.Application.Interfaces;
using Enrollment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Enrollment.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly EnrollmentContext _context;

    public EnrollmentRepository(EnrollmentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsDetailsByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Enrollments.Include(_ => _.UserEnrollments)
            .FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
    }

    public async Task<TrainingEnrollmentsDetails?> GetTrainingEnrollmentsWithoutUserEnrollmentsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Enrollments.FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
    }

    public async Task<List<UserEnrollment>> GetUserEnrollmentByUserAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.UserEnrollments.Where(_ => _.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(TrainingEnrollmentsDetails trainingEnrollmentsDetails,
        CancellationToken cancellationToken)
    {
        await _context.Enrollments.AddAsync(trainingEnrollmentsDetails, cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}