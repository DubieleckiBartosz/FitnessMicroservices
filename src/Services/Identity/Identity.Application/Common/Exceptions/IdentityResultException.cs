namespace Identity.Application.Common.Exceptions;

public class IdentityResultException : FitnessApplicationException
{
    public List<string>? Errors { get; }

    public IdentityResultException(string messageDetail, string title, HttpStatusCode statusCode,
        List<string>? errors) : base(messageDetail, title, statusCode)
    {
        Errors = errors;
    }
}