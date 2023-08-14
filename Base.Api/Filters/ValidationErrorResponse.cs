namespace Base.Api.Filters
{
    /// <summary>
    /// Response to validation of request
    /// </summary>
    /// <param name="Code"></param>
    /// <param name="Message"></param>
    /// <param name="Errors"></param>
    
    public record ValidationErrorResponse(int Code, string Message, string[] Errors);

}
