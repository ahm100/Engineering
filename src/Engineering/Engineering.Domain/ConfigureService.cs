using Gita.Backend.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Engineering.Domain;

public static class ConfigureService
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddSharedDomainServices(assemblies);
        AddServiceScopes(services);
        return services;
    }

    private static void AddServiceScopes(IServiceCollection services)
    {
    }
}