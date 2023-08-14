using Base.Infrastructure.Extensions.Cors;
using Base.Infrastructure.Extensions.Log;
using Base.Infrastructure.Extensions.Mapper;
using Base.Infrastructure.Extensions.Mediator;
using Base.Infrastructure.Extensions.Messaging;
using Base.Infrastructure.Extensions.OpenApi;
using Base.Infrastructure.Extensions.Persistence;
using Base.Infrastructure.Extensions.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddOpenApiDocumentation(config)
            .AddMediator()
            .AddMapper()
            .AddContextDatabase(config)
            .AddCors()
            .AddLogger()
            .AddPersistence(config)
            .AddDomainServices()
            .AddRabbitSupport(config);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IWebHostEnvironment env) =>
        builder
            .UseOpenApiDocumentation(env)
            .UseCorsPolicy();
}