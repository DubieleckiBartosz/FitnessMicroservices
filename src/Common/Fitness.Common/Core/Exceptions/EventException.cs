namespace Fitness.Common.Core.Exceptions;

public class EventException : FitnessApplicationException
{
    public EventException(string messageDetail, string title,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(messageDetail,
        title, statusCode)
    {
    }
}