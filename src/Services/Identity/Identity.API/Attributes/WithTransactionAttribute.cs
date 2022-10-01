namespace Identity.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WithTransactionAttribute : Attribute
    {
    }
}
