using Api.Filters;
using Infrastructure;
using Infrastructure.Extensions;
using Infrastructure.Extensions.Persistence;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
if (builder.Environment.IsEnvironment(ApiConstants.LocalEnviroment))
{
    config.AddUserSecrets<Program>();
}

builder.Services.Configure<DatabaseSettings>(config.GetSection(nameof(DatabaseSettings)));
var settings = config.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
builder.Services.AddHealthChecks().AddSqlServer(settings.ConnectionString);
builder.Services.AddControllers(opts => opts.Filters.Add(typeof(AppExceptionFilterAttribute)));
builder.Services.AddInfrastructure(config);

builder.Services.AddEndpointsApiExplorer();

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var app = builder.Build();
app.UseRouting().UseHttpMetrics().UseEndpoints(endpoints =>
{
    endpoints.MapGet("/palm/base-version", () => new { version = 1.0, by = "Finotex" });
    endpoints.MapMetrics();
    endpoints.MapHealthChecks("/health");
});

app.UseInfrastructure(app.Environment);

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }