namespace Fitness.Common.Projection;

public interface IRead
{
    public Guid Id { get; }
    public bool IsDeleted { get; set; }
}