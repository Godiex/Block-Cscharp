using System.Net;
using Base.Application.Common.Exceptions;
using Base.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using ValidationException = FluentValidation.ValidationException;

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

        object errorResponse;

        if (context.Exception is ValidationException validationException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errors = validationException.Errors.Select(e => e.ErrorMessage).ToArray();
            errorResponse = new ValidationErrorResponse((int)HttpStatusCode.BadRequest, "Errores de validación", errors);
        }
        else
        {
            context.HttpContext.Response.StatusCode = context.Exception switch
            {
                CoreBusinessException => (int)HttpStatusCode.BadRequest,
                NotFoundException => (int)HttpStatusCode.NotFound,
                ForbiddenException => (int)HttpStatusCode.Forbidden,
                AlreadyExistException => (int)HttpStatusCode.BadRequest,
                ConflictException => (int)HttpStatusCode.Conflict,
                CustomException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            _Logger.LogError(context.Exception, context.Exception.Message, context.Exception.StackTrace);
            errorResponse = new ValidationErrorResponse(context.HttpContext.Response.StatusCode, context.Exception.Message, new []{ context.Exception.InnerException.Message });
        }

        context.Result = new ObjectResult(errorResponse);
    }

}