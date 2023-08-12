using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api
{
    public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
    {
        public PersistenceContext CreateDbContext(string[] args)
        {
             var Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                
            var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
            optionsBuilder.UseSqlServer(Config.GetConnectionString("database"), sqlopts =>
            {
                sqlopts.MigrationsHistoryTable("_MigrationHistory", Config.GetValue<string>("SchemaName"));
            });

            return new PersistenceContext(optionsBuilder.Options, Config);
        }
    }
}