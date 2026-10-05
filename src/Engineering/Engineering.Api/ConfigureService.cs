using Commercial.Api.Services;
using Engineering.Api.Controllers.Projects.Reports;
using Engineering.Api.Controllers.RequestGoodsSupplies.Reports.GetDetailByRGSIdReport;
using Engineering.Api.Controllers.RequestGoodsSupplyDetails.Reports;
using Engineering.Api.Helpers.MppTools;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.ClientSdk;
using Gita.Backend.Shared.Api;
using Gita.Backend.Shared.Api.Middlewares;
using Gita.Backend.Shared.Application.Extensions;
using Gita.Backend.Shared.Domain.Services;
using Gita.Shared.FileGenerators;
using IdentityServer.ClientSdk;
using IdentityServer.ClientSdk.Configs;
using MessageSender.ClientSdk;
using MessageSender.ClientSdk.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Any;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using Warehouse.ClientSdks;

namespace Engineering.Api;

/// <summary>
/// 
/// </summary>
public static class ConfigureService
{
    public static bool SkipAddEngineeringClientSdk = false;
    private static readonly string[] ConfigureOptions = ["en-US", "fa-IR"];

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = ConfigureOptions;
            options.SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });

        AddApiVersion(services);
        services.AddControllers();

        services.AddSingleton(() => DateTime.UtcNow);
        ConfigureIdentityServer(services, configuration, environment);

        services.AddFileGenerators(configuration, new FileGeneratorsConfig
        {
            FontsDirectoryDefault = "Reports/Fonts",
            ReportsDirectoryDefault = "Reports"
        });

        services.AddWarehouseClientSdk(configuration, environment);

        if (!SkipAddEngineeringClientSdk)
        {
            services.AddEngineeringClientSdk(configuration, environment);
        }

        services.AddEndpointsApiExplorer();

        AddSwaggerService(services);
        services.AddSwaggerGen();

        AddCorsService(services);

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        services.AddFluentValidationAutoValidation();

        services.AddMediatR(x =>
        {
            x.Lifetime = ServiceLifetime.Scoped;
            x.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        });

        services.AddCarter();
        services.AddSharedApiServices(configuration);
        services.AddScoped<TemplateService>();
        services.AddScoped<GetsPdfGoodsSupplyProductHandle>();
        services.AddScoped<GetsGroupPdfGoodsSupplyProductHandle>();
        services.AddScoped<GetRGSProductXlsxReportHandle>();
        services.AddScoped<GetPdfProjectContractsHandle>();
        services.AddScoped<GetPdfProjectContractorHumanResourcesHandle>();
        services.AddScoped<GetPdfProjectEmployerHumanResourcesHandle>();
        services.AddScoped<GetPdfProjectByIdHandle>();
        services.AddScoped<GetDetailByRGSIdXslxReportHandle>();
        services.AddScoped<GetReferenceTypeHistoryFaReportHandle>();
        services.AddScoped<GetDetailByRGSIdXslxEnReportHandle>();
        services.AddScoped<GetDetailByRGSIdPdfReportHandle>();
        services.AddScoped<GetDetailByRGSIdPdfEnReportHandle>();

        services.AddSingleton<ITempFileProvider, TempFileProvider>();
        services.AddSingleton<UnhandledExceptionMiddleware>();

        services.AddScoped<IMppParser, MppParser>();

        services.AddScoped<Gita.Backend.Shared.Domain.Services.IAppJsonSerializer, AppJsonSerializer>();
        services.AddScoped<IOutboxStore, OutboxStore>();
        services.AddScoped<IMessageRelay, MessageRelay>();
        services.AddMessageSenderClientSdk(configuration, environment, new MessageSenderClientSdkConfig());

        var redisConfig = configuration.GetSection("RedisConfiguration").Get<RedisConfiguration>();

        if (redisConfig is not null)
        {
            var configOptions = new ConfigurationOptions
            {
                EndPoints = { { redisConfig.Hosts[0].Host, redisConfig.Hosts[0].Port } },
                Password = redisConfig.Password,
                AbortOnConnectFail = false
            };

            var connection = ConnectionMultiplexer.Connect(configOptions);
            services.AddSingleton<IConnectionMultiplexer>(connection);
        }

        return services;
    }

    private static void ConfigureIdentityServer(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var identityConfig = configuration.RegisterAndGetConfiguration<IdentityConfig>(
            services, nameof(IdentityConfig));

        identityConfig.ValidateAudience = false;

        services.AddIdentityAuthentication(configuration, environment, identityConfig);
        services.AddIdentityClientSdk(configuration, environment, new());
    }

    private static void AddCorsService(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(p => p
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );
        });
    }

    private static void AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("api-version"),
                new MediaTypeApiVersionReader("api-version"));
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddApiApplication(this WebApplication app)
    {
        app.UseSharedApiServices();
        app.AddUiSwaggerService();

        app.UseUnhandledExceptionMiddleware();
        // app.UseHttpsRedirection();
        app.UseCors();

        app.UseIdentityAuthentication();

        // app.AddTokenValidate();
        app.UseAuthorization();
        app.MapControllers();
        app.MapCarter();

        return app;
    }

    private static void AddSwaggerService(IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1.0", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Engineering Server Api",
                Description = "engineering Api v1.0"
            });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.SupportNonNullableReferenceTypes();
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            opt.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            });

        });
    }

    private static void AddUiSwaggerService(this IApplicationBuilder app)
    {
        app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/engineering/{documentname}/swagger.json";
            c.PreSerializeFilters.Clear();
            c.PreSerializeFilters.Add((swagger, httpRequest) =>
            {
                swagger.Servers = new List<OpenApiServer>();
            });
        });
        app.UseSwaggerUI(config =>
        {
            config.SwaggerEndpoint("/swagger/engineering/v1.0/swagger.json", "Engineering Api v1.0");
            config.RoutePrefix = "swagger/engineering";
        });
    }

}
