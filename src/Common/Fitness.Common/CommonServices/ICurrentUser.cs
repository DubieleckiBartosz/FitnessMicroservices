namespace Fitness.Common.CommonServices;

public interface ICurrentUser
{
    bool IsInRole(string roleName);
    int UserId { get; }
    string? TrainerCode { get; }
}