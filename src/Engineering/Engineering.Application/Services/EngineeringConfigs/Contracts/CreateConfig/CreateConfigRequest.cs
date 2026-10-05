namespace Engineering.Application.Services.EngineeringConfigs.Contracts.CreateConfig;

public record CreateConfigRequest(
    bool SendTelegramMessage,
    bool ProjectThirdParties,
    bool? IsActive
     ) : IHttpRequest;