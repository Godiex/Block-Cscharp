namespace Infrastructure.Extensions.Persistence;

public class DatabaseSettings
{
    public string DbProvider { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string MigrationsHistoryTable { get; set; } = string.Empty;
    public string SchemaName { get; set; } = string.Empty;
}