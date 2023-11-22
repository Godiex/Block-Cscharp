using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Localization;

public static class LocalizationExtensions {
    public static IServiceCollection AddLocalizationMessages(this IServiceCollection svc) {
        var domainLayerPath = GetPathDomainLayer();
        svc.AddLocalization(options => options.ResourcesPath = domainLayerPath);
        svc.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("es"),
            };

            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        svc.AddSingleton<LocalizationMiddleware>();
        return svc;
    }

    private static string GetPathDomainLayer()
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var projectDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory!, @"..\..\..\"));
        var domainLayerPath = Path.Combine(projectDirectory, ApiConstants.DomainProject);
        return domainLayerPath;
    }

    public static IApplicationBuilder UseLocalizationMessages(this IApplicationBuilder app)
    {
        app.UseRequestLocalization();
        app.UseMiddleware<LocalizationMiddleware>();
        return app;
    }
}