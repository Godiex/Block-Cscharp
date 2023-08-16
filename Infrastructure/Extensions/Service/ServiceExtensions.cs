using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Service;

public static class ServiceExtensions {
    public static IServiceCollection AddDomainServices(this IServiceCollection svc)
    {
        var services = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => (assembly.FullName is not null) && assembly.FullName.Contains("Domain", StringComparison.InvariantCulture))
            .SelectMany(s => s.GetTypes())
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

        foreach (var service in services)
        {
            svc.AddTransient(service);
        }

        return svc;
    }
}