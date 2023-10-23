using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Extensions.Log;

public static class LoggerExtensions {
    public static IServiceCollection AddLogger(this IServiceCollection svc) {
        svc.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        return svc;
        
    }
}