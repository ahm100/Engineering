namespace Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateConfig;

public record UpdateConfigRequest(
    long Id,
    bool SendTelegramMessage,
    bool ProjectThirdParties,
    bool? IsActive
     ) : IHttpRequest;
