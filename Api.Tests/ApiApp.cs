using Infrastructure.Context;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Api.Tests;

public class ApiApp : WebApplicationFactory<Program>
{
    public Guid UserId { get; } = Guid.NewGuid();

    // We should use this service collection to access repos and seed data for tests
    public IServiceProvider GetServiceCollection()
    {
        return Services;           
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(svc =>
        {
            svc.RemoveAll(typeof(DbContextOptions<PersistenceContext>));
            svc.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseInMemoryDatabase("testdb");
            });

        });

        return base.CreateHost(builder);
    }

    
}

