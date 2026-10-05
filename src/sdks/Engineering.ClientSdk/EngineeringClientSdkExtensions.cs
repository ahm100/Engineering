using Engineering.ClientSdk.Helpers;
using Engineering.ClientSdk.Services;
using Engineering.ClientSdk.Services.HttpImpl;
using IdentityServer.ClientSdk.Services.IdentityManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Engineering.ClientSdk;

public static class EngineeringClientSdkExtensions
{
    public static void AddEngineeringClientSdk(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var engineeringBaseUrl = GetUrl(configuration, "EngineeringConfig:Server");

        services.AddScoped<HttpTransportationContractorService>()
          .AddHttpClient<HttpTransportationContractorService>(httpClient => httpClient.BaseAddress = new Uri(engineeringBaseUrl));

        services.AddEngineeringClientSdkSharedServices(configuration);
    }

    public static void AddEngineeringClientSdk(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment, HttpClient httpClient)
    {
        services.AddScoped<HttpTransportationContractorService>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<HttpTransportationContractorService>>();
            var serializer = sp.GetRequiredService<IEngineeringClientSdkJsonSerializer>();
            var tokenProvider = sp.GetRequiredService<ITokenProvider>();
            return new HttpTransportationContractorService(httpClient, serializer, tokenProvider, logger);
        });

        services.AddEngineeringClientSdkSharedServices(configuration);
    }

    private static void AddEngineeringClientSdkSharedServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IEngineeringClientSdkJsonSerializer, EngineeringClientSdkJsonSerializer>();

        services.AddScoped<ITransportationContractorService>(sp => sp.GetRequiredService<HttpTransportationContractorService>());
    }

    private static string GetUrl(IConfiguration configuration, string configPath)
    {
        var configUrl = configuration[configPath];
        if (string.IsNullOrEmpty(configUrl))
        {
            throw new InvalidOperationException($"A valid server url for {configPath} is required.");
        }

        var serverBaseUrl = InternalHelpers.GetCanonicalBaseAddress(configUrl);

        return serverBaseUrl;
    }
}
