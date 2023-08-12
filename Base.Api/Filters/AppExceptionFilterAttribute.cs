using System.Net;
using Base.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Api.Filters;

[AttributeUsage(AttributeTargets.All)]
public sealed class AppExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<AppExceptionFilterAttribute> _Logger;

    public AppExceptionFilterAttribute(ILogger<AppExceptionFilterAttribute> logger)
    {
        _Logger = logger;
    }


    public override void OnException(ExceptionContext context)
    {
        if (context == null) return;
        context.HttpContext.Response.StatusCode = context.Exception switch
        {
            CoreBusinessException => ((int)HttpStatusCode.BadRequest),
            _ => ((int)HttpStatusCode.InternalServerError)
        };

        _Logger.LogError(context.Exception, context.Exception.Message, new[] { context.Exception.StackTrace });

        var msg = new
        {
            context.Exception.Message
        };

        context.Result = new ObjectResult(msg);
    }

}