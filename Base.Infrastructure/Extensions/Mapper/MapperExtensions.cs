using System.Reflection;
using Base.Infrastructure.Extensions.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions.Mapper;

public static class MapperExtensions 
{
    public static IServiceCollection AddMapper(this IServiceCollection svc) {
        svc.AddAutoMapper(Assembly.Load(ProjectsName.Application));
        return svc;
    }
}