using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Infrastructure.Extensions.OpenApi;

public static class OpenApiDocumentationExtensions {
    public static IServiceCollection AddOpenApiDocumentation(this IServiceCollection svc) {
        svc.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        svc.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        svc.AddSwaggerGen();
        svc.AddEndpointsApiExplorer();
        return svc.AddSwaggerGen(o =>
        {
            const string companyUrl = "https://www.finotex.com/";
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Base api finotex",
                Description = "Configuracion de api para qr custom link",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Finotex",
                    Email = "infousa@finotex.com",
                    Url = new Uri(companyUrl)
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            o.ExampleFilters();
        });
    }

    public static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment() && !env.IsEnvironment(ApiConstants.LocalEnviroment)) return app;
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Block Api"));
        return app;
    }
}