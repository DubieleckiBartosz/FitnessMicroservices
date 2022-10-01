namespace Fitness.Common.Core.Exceptions;

public class FitnessApplicationException : Exception
{
    public string Title { get; }
    public HttpStatusCode StatusCode { get; }


    public FitnessApplicationException(string messageDetail, string title, HttpStatusCode statusCode) : base(
        messageDetail)
    {
        Title = title;
        StatusCode = statusCode;
    }
}