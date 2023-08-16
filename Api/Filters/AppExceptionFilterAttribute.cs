using System.Net;
using Application.Common.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

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
            ValidationException  => (int)HttpStatusCode.BadRequest,
            CoreBusinessException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.NotFound,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            AlreadyExistException => (int)HttpStatusCode.BadRequest,
            ConflictException => (int)HttpStatusCode.Conflict,
            CustomException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        _Logger.LogError(context.Exception, context.Exception.Message, context.Exception.StackTrace);
        var errorResponse = new ErrorResponse(context.HttpContext.Response.StatusCode, context.Exception.Message, context.Exception.GetType().Name);

        context.Result = new ObjectResult(errorResponse);
    }

}