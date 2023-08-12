using System.Reflection;
using Base.Infrastructure.Extensions.Helper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions.Mediator;

public static class MediatorExtensions {
    public static IServiceCollection AddMediator(this IServiceCollection svc) {
        svc.AddMediatR(Assembly.Load(ProjectsName.Application), Assembly.GetExecutingAssembly());
        return svc;
    }
}