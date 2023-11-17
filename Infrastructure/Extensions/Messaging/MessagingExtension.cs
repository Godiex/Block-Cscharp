using Application.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Infrastructure.Extensions.Messaging;

public static class MessagingExtension
{
    public static IServiceCollection AddMessageSupport(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }
}