namespace Opinion.API.Domain.Common;

public abstract class BaseEntity
{
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}