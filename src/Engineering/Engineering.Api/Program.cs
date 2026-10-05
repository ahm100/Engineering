using Engineering.Api;
using Engineering.Application;
using Engineering.Domain;
using Engineering.Infra;
using Engineering.Persistence;
using Gita.Backend.Shared.Domain.Hosting;
using Gita.Backend.Shared.Infra.Services;
using Gita.Shared.Observability;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(20);
});

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

builder.Services
    .AddDomainServices(typeof(Program).Assembly,
        typeof(Gita.Backend.Shared.Domain.ConfigureServices).Assembly,
        typeof(Gita.Backend.Shared.Application.ConfigureServices).Assembly,
        typeof(Gita.Backend.Shared.Infra.ConfigureServices).Assembly,
        typeof(Engineering.Application.ConfigureService).Assembly,
        typeof(Engineering.Domain.ConfigureService).Assembly,
        typeof(Engineering.Infra.ConfigureService).Assembly)

    .AddApplicationServices([
        typeof(Program).Assembly,
        typeof(Gita.Backend.Shared.Domain.ConfigureServices).Assembly,
        typeof(Gita.Backend.Shared.Application.ConfigureServices).Assembly,
        typeof(Gita.Backend.Shared.Infra.ConfigureServices).Assembly,
        typeof(Engineering.Application.ConfigureService).Assembly
    ], builder.Configuration)

    .AddInfraServices(builder.Configuration, builder.Logging)
    .AddPersistenceServices(builder.Configuration)
    .AddApiServices(builder.Configuration, builder.Environment);

if (bool.TryParse(builder.Configuration["ObservabilityOptions:IsEnabled"], out var observabilityIsEnabled) &&
    observabilityIsEnabled)
{
    builder.AddObservability(new HostInfoProvider(
    nameof(Engineering),
    "1.0.0",
    "1.0.0",
    "1.0.0",
    "1.0.0",
    "1.0.0",
    1,
    "1.0.0"
));
}

var app = builder.Build();
app.AddHealthCheckEndpoint();
app.AddApiApplication();
app.MapControllers().RequireAuthorization();

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EngineeringDBContext>();
    await dbContext.Database.MigrateAsync();
}

await app.RunAsync();

public partial class Program
{
}