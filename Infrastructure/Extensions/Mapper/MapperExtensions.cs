using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Mapper;

public static class MapperExtensions 
{
    public static IServiceCollection AddMapper(this IServiceCollection svc) {
        TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
        return svc;
    }
}