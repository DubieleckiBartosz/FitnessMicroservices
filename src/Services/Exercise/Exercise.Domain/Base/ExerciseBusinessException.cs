using System.Net;

namespace Exercise.Domain.Base;

public class ExerciseBusinessException : Exception
{
    public string Title { get; }
    public HttpStatusCode StatusCode { get; }
     
    public ExerciseBusinessException(string messageDetail, string title) : base(
        messageDetail)
    {
        Title = title;
        StatusCode = HttpStatusCode.BadRequest;
    }
}