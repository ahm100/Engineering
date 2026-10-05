using Engineering.Application.Abstractions.Interfaces;
using Engineering.Infra.MessageBus.Receivers;
using Engineering.Infra.Providers;
using Engineering.Infra.Providers.Commercial;
using Engineering.Infra.Providers.HumenResource;
using Engineering.Infra.Providers.Identity;
using Engineering.Infra.Providers.MessageSender;
using Engineering.Infra.Providers.MetaData;
using Engineering.Infra.Providers.ObjectStorage;
using Engineering.Infra.Providers.PdfMaker;
using Engineering.Infra.Providers.Treasury;
using Engineering.Infra.Providers.Warehouse;
using Engineering.Infra.WorkflowRequests;
using Financial.Application.Abstractions.Interfaces;
using Financial.Infra.Providers.PdfMaker;
using Gita.Backend.Shared.Infra;
using Gita.Backend.Shared.Infra.Extension;
using Gita.Backend.Shared.Infra.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Engineering.Infra;

public static class ConfigureService
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services,
        IConfiguration configuration, ILoggingBuilder logging)
    {
        ConfigureServices.AddSharedInfraServices(services, configuration, logging);

        services.AddRabbitMQ(configuration, cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddConsumersFromNamespaceContaining<TreasuryPettyCashResponseMessageConsumer>();
            cfg.AddConsumersFromNamespaceContaining<TreasuryPaymentOrderResponseMessageConsumer>();
            cfg.AddConsumersFromNamespaceContaining<WarehouseInvoiceResponseMessageConsumer>();
            cfg.AddConsumersFromNamespaceContaining<CommercialResponseMessageConsumer>();
        });

        AddServiceScopes(services);
        AddRefitServices(services, configuration);
        AddPollyResiliencePipeline(services, configuration);
        services.AddWorkflowIntegration(configuration);

        return services;
    }

    private static void AddRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        AddMetaDataRefitServices(services, configuration);
        AddObjectStorageRefitServices(services, configuration);
        AddIdentityRefitServices(services, configuration);
        AddHumanResourceRefitServices(services, configuration);
        AddWarehouseRefitServices(services, configuration);
        AddCommercialRefitServices(services, configuration);
        AddTreasuryRefitServices(services, configuration);
        AddMessageSenderRefitServices(services, configuration);
        AddPdfMakerRefitServices(services, configuration);
    }

    private static void AddMetaDataRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<MetaDataConfig>(services, "MetaDataConfig");

        services
            .AddRefitClient<IMetaDataProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<MetaDataLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddObjectStorageRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<ObjectStorageConfig>(services, "ObjectStorageConfig");

        services
            .AddRefitClient<IObjectStorageProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<ObjectStorageLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddCommercialRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<CommercialConfig>(services, "CommercialConfig");

        services
            .AddRefitClient<ICommercialProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<CommercialLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }


    private static void AddPdfMakerRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<PdfMakerConfig>(services, "PdfMakerConfig");

        services
            .AddRefitClient<IPdfMakerProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<PdfMakerLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddTreasuryRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<TreasuryConfig>(services, "TreasuryConfig");

        services
            .AddRefitClient<ITreasuryProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<TreasuryLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddIdentityRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<IdentityConfig>(services, "AuthenticationConfig");

        services
            .AddRefitClient<IIdentityProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<IdentityLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddHumanResourceRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<HumenResourceConfig>(services, "HumanResourceConfig");

        services
            .AddRefitClient<IHumenResourceProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<HumenResourceLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddMessageSenderRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<MessageSenderConfig>(services, "MessageSenderConfig");

        services
            .AddRefitClient<IMessageSenderProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<MessageSenderLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddWarehouseRefitServices(IServiceCollection services, IConfiguration configuration)
    {
        var configs = configuration.RegisterAndGetConfiguration<WarehouseConfig>(services, "WarehouseConfig");

        services
            .AddRefitClient<IWarehouseProvider>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configs.Server))
            .AddHttpMessageHandler<WarehouseLoggingHandler>()
#if DEBUG
            .AddHttpMessageHandler<HttpLoggingHandler>()
#endif
            ;
    }

    private static void AddPollyResiliencePipeline(IServiceCollection services, IConfiguration configuration)
    {
        services.AddResiliencePipeline<string, Result>("RetryPipeline", builder =>
        {
            builder.AddRetry(new RetryStrategyOptions<Result>
            {
                ShouldHandle = new PredicateBuilder<Result>().Handle<DbUpdateConcurrencyException>()
                                                             .HandleResult(r => r.IsFailure),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                MaxRetryAttempts = 5,
                Delay = TimeSpan.FromSeconds(5),
            })
           .AddTimeout(TimeSpan.FromSeconds(10));
        });
    }

    private static void AddServiceScopes(IServiceCollection services)
    {

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IdentityLoggingHandler>();

        services.AddScoped<IMetaDataService, MetaDataService>();
        services.AddScoped<MetaDataLoggingHandler>();

        services.AddScoped<IObjectStorageService, ObjectStorageService>();
        services.AddScoped<ObjectStorageLoggingHandler>();

        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<WarehouseLoggingHandler>();

        services.AddScoped<ICommercialService, CommercialService>();
        services.AddScoped<CommercialLoggingHandler>();

        services.AddScoped<ITreasuryService, TreasuryService>();
        services.AddScoped<TreasuryLoggingHandler>();

        services.AddScoped<IMessageSenderService, MessageSenderService>();
        services.AddScoped<MessageSenderLoggingHandler>();

        services.AddSingleton<IDateTime, DateTimeImp>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddTransient<IPdfMakerService, PdfMakerService>();
        services.AddTransient<PdfMakerLoggingHandler>();

        services.AddScoped<IHumenResourceService, HumenResourceService>();
        services.AddScoped<HumenResourceLoggingHandler>();
    }
}
