using Infrastructure.Context;
using Infrastructure.Extensions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Api;

public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
{
    public PersistenceContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .Build();
        
        var serviceProvider = new ServiceCollection()
            .AddOptions()
            .Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)))
            .BuildServiceProvider();

        var databaseSettingsOptions = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>();
        var databaseSettings = databaseSettingsOptions.Value;
        
        var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
        optionsBuilder.UseSqlServer(databaseSettings.ConnectionString, sqlopts =>
        {
            sqlopts.MigrationsHistoryTable(databaseSettings.MigrationsHistoryTable, databaseSettings.SchemaName);
        });

        return new PersistenceContext(optionsBuilder.Options, databaseSettingsOptions);
    }
}