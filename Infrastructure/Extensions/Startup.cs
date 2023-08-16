using Infrastructure.Extensions.Cors;
using Infrastructure.Extensions.Log;
using Infrastructure.Extensions.Mapper;
using Infrastructure.Extensions.Mediator;
using Infrastructure.Extensions.Messaging;
using Infrastructure.Extensions.OpenApi;
using Infrastructure.Extensions.Persistence;
using Infrastructure.Extensions.Service;
using Infrastructure.Extensions.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddOpenApiDocumentation()
            .AddValidation()
            .AddMediator()
            .AddMapper()
            .AddContextDatabase(config)
            .AddCors()
            .AddLogger()
            .AddPersistence(config)
            .AddDomainServices()
            .AddRabbitSupport(config);
    }

    public static void UseInfrastructure(this IApplicationBuilder builder, IWebHostEnvironment env)
    {
        builder
            .UseOpenApiDocumentation(env)
            .UseCorsPolicy();
    }
}