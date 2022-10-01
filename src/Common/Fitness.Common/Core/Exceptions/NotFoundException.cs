namespace Fitness.Common.Core.Exceptions
{
    public class NotFoundException : FitnessApplicationException
    {
        public NotFoundException(string messageDetail, string title) : base(messageDetail,
            title, HttpStatusCode.NotFound)
        {
        }
    }
}
