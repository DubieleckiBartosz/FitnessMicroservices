namespace Fitness.Common.Core.Exceptions;

public class BadRequestException : FitnessApplicationException
{
    public BadRequestException(string messageDetail, string title) : base(messageDetail,
        title, HttpStatusCode.BadRequest)
    {
    }
}