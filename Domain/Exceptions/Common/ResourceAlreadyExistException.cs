using System.Runtime.Serialization;

namespace Domain.Exceptions.Common;

public class ResourceAlreadyExistException : CoreBusinessException
{
    public ResourceAlreadyExistException()
    {
    }

    public ResourceAlreadyExistException(string msg) : base(msg)
    {
    }

    public ResourceAlreadyExistException(string message, Exception inner) : base(message, inner)
    {
    }

    private  ResourceAlreadyExistException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}