namespace Identity.Domain.Base.Business;

public class BusinessException : Exception
{
    public string Title { get; set; }
    public BusinessException(string message, string title) : base(message)
    {
        Title = title;
    }
}