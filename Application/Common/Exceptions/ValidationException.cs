namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ILookup<string, string> Errors { get; } = default!;
    private const string defaultMessage = "Validación no superada";

    public ValidationException(string message)
        : this(message, Enumerable.Empty<string>().ToLookup(_ => ""))
    { }

    public ValidationException(ILookup<string, string> errors)
        : this(defaultMessage, errors)
    { }

    public ValidationException(string message, ILookup<string, string> errors)
        : base(message)
    {
        Errors = errors;
    }
}