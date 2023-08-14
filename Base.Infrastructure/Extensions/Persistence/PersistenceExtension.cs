using System.Data;
using Base.Domain.Ports;
using Base.Infrastructure.Adapters;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions.Persistence;

public static class PersistenceExtensions {
    public static IServiceCollection AddPersistence(this IServiceCollection svc, IConfiguration config) {
        svc.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        svc.AddTransient<IDbConnection>((sp) => new SqlConnection(config.GetConnectionString("database")));
        return svc;
    }
}