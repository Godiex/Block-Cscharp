using System.Reflection;
using Base.Infrastructure.Extensions.Helper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions.Mediator;

public static class MapperExtensions {
    public static IServiceCollection AddMapper(this IServiceCollection svc) {
        svc.AddAutoMapper(Assembly.Load(ProjectsName.Application));
        return svc;
    }
}