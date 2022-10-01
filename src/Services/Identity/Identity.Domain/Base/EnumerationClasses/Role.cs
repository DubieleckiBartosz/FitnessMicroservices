namespace Identity.Domain.Base.EnumerationClasses;

public class Role : Enumeration
{
    public static Role Admin = new(1, nameof(Admin));
    public static Role User = new(2, nameof(User));
    public static Role Trainer = new(3, nameof(Trainer));

    protected Role(int id, string name) : base(id, name)
    {
    }
}