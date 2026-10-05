using Engineering.Application.Services.WorkflowRequests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engineering.Infra.WorkflowRequests;

public static class WorkflowIntegrationRegistration
{
    /// <summary>Logic مشترک، Handler هزینه بالاسری و انتقال REST را ثبت می کند.</summary>
    public static IServiceCollection AddWorkflowIntegration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<WorkflowIntegrationOptions>(configuration.GetSection(WorkflowIntegrationOptions.SectionName));

        services.AddSingleton<WorkflowCallbackVerifier>();

        services.AddHttpClient<IWorkflowTransport, RestWorkflowTransport>(client => client.Timeout = TimeSpan.FromSeconds(60))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AllowAutoRedirect = false });

        services.AddHostedService<WorkflowOutboxWorker>();
        return services;
    }
}
