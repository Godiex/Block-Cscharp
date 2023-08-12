using Base.Api.Filters;
using Base.Infrastructure.Context;
using Base.Infrastructure.Extensions;
using Base.Infrastructure.Inicialize;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddHealthChecks().AddSqlServer(config["ConnectionStrings:database"]);
builder.Services.AddInfrastructure(config);

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(AppExceptionFilterAttribute));
});

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

using var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
var contex = scope!.ServiceProvider.GetRequiredService<PersistenceContext>();
var start = new Start(contex);
start.Inicializar();

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
