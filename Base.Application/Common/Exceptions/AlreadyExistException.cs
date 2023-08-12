using System.Net;

namespace Base.Application.Common.Exceptions;

public class AlreadyExistException : CustomException
{
    public AlreadyExistException(string message)
        : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}