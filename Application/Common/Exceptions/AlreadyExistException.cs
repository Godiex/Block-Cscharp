namespace Application.Common.Exceptions;

public class AlreadyExistException : CustomException
{
    public AlreadyExistException(string message)
        : base(message, null)
    {
    }
}