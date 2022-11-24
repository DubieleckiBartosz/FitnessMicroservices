namespace Enrollment.Application.Interfaces;

public interface IWrapperRepository
{
    public IEnrollmentRepository EnrollmentRepository { get; }
}