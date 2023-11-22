using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Extensions.Localization;

public class LocalizationMiddleware : IMiddleware
{
    private static readonly HashSet<string> ValidCultures = new(StringComparer.OrdinalIgnoreCase)
    {
        "es",
        "en"
    };
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureKey = context.Request.Headers["Accept-Language"];
        if (!string.IsNullOrEmpty(cultureKey) && CultureValid(cultureKey))
        {
            var culture = new CultureInfo(cultureKey);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        await next(context);
    }

    private static bool CultureValid(string cultureName)
    {
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return false;
        }

        if (!ValidCultures.Contains(cultureName))
        {
            return false;
        }

        var neutralCultureName = CultureInfo.GetCultureInfo(cultureName).IsNeutralCulture
            ? cultureName
            : CultureInfo.GetCultureInfo(cultureName).Parent.Name;

        return ValidCultures.Contains(neutralCultureName);
    }
}