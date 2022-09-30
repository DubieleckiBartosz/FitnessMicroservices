namespace Identity.Application.Common.Exceptions;

public class IdentityApplicationException : Exception
{
    public string Title { get; }
    public HttpStatusCode StatusCode { get; }


    public IdentityApplicationException(string messageDetail, string title, HttpStatusCode statusCode) : base(
        messageDetail)
    {
        Title = title;
        StatusCode = statusCode; 
    } 
}