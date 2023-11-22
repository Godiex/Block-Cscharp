using System.Runtime.Serialization;

namespace Domain.Exceptions.Common;

public class ResourceNotFoundException : CoreBusinessException
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string msg) : base(msg)
    {
    }

    public ResourceNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    private  ResourceNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}