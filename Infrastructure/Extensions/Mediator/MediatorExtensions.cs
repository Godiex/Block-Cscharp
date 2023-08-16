using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection svc) {
        svc.AddMediatR(Assembly.Load(ApiConstants.ApplicationProject), Assembly.GetExecutingAssembly());
        return svc;
    }
}