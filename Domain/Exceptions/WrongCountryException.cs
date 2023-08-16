using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public sealed class WrongCountryException : CoreBusinessException
{
    public WrongCountryException()
    {
    }
    public WrongCountryException(string msg) : base(msg)
    {
    }

    public WrongCountryException(string message, Exception inner) : base(message, inner)
    {
    }

    private WrongCountryException(SerializationInfo info, StreamingContext context)
   : base(info, context)
    {
    }


}
