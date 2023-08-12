namespace Epm.Renovables.Maestros.Api.Filtros
{
    /// <summary>
    /// Response to validation of request
    /// </summary>
    /// <param name="Code"></param>
    /// <param name="Message"></param>
    /// <param name="Errors"></param>
    /// <param name="InnerMessage"></param>
    public record ValidationErrorResponse(int Code, string Message, ILookup<string, string> Errors, object? InnerMessage);
}
