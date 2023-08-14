using System.Runtime.Serialization;

namespace Base.Application.Common.Exceptions
{
    [Serializable]
    public class ValidationException : CustomException
    {
        public ILookup<string, string> Errors { get; }

        private const string DefaultMessage = "Validación no superada";

        public ValidationException(string message)
            : this(message, Enumerable.Empty<string>().ToLookup(_ => ""))
        { }

        public ValidationException(ILookup<string, string> errors)
        : this(DefaultMessage, errors)
        { }

        public ValidationException(string message, ILookup<string, string> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}