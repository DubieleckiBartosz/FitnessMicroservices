namespace Training.API.Common.Exceptions;

public class TrainingServiceBusinessException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }

    public TrainingServiceBusinessException(string title, string message,
        int statusCode = StatusCodes.Status400BadRequest) :
        base(message)
    {
        StatusCode = statusCode;
        Title = title;
    }
}