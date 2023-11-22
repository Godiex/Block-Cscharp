using Infrastructure.Extensions.Cors;
using Infrastructure.Extensions.Localization;
using Infrastructure.Extensions.Logs;
using Infrastructure.Extensions.Mapper;
using Infrastructure.Extensions.Mediator;
using Infrastructure.Extensions.Messaging;
using Infrastructure.Extensions.OpenApi;
using Infrastructure.Extensions.Persistence;
using Infrastructure.Extensions.Service;
using Infrastructure.Extensions.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddLocalizationMessages()
            .AddOpenApiDocumentation()
            .AddValidation()
            .AddMediator()
            .AddMapper()
            .AddPersistence(config)
            .AddCorsPolicy(config)
            .AddLogger()
            .AddRepositories(config)
            .AddDomainServices()
            .AddMessageSupport(config);
    }

    public static void UseInfrastructure(this IApplicationBuilder builder)
    {
        builder
            .UseLocalizationMessages()
            .UseOpenApiDocumentation()
            .UseCorsPolicy();
    }
}