using System.Globalization;
using System.Reflection;
using FluentValidation;
using Infrastructure.Adapters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Validation;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection svc)
    {
        svc.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        var applicationAssembly = Assembly.Load(ApiConstants.ApplicationProject);
        svc.AddValidatorsFromAssembly(applicationAssembly);
        svc.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return svc;
    }
}