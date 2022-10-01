namespace Fitness.Common.Core.Exceptions
{
    public class DatabaseException : FitnessApplicationException
    {
        public DatabaseException(string messageDetail, string title, HttpStatusCode statusCode) : base(messageDetail,
            title, statusCode)
        {
        }
    }
}
