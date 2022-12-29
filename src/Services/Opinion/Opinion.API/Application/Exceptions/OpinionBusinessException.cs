using System.Net;
using Fitness.Common.Core.Exceptions;

namespace Opinion.API.Application.Exceptions;

public class OpinionBusinessException : FitnessApplicationException
{
    public OpinionBusinessException(string messageDetail, string title, HttpStatusCode statusCode) : base(messageDetail,
        title, statusCode)
    {
    }
}