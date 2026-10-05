namespace Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteCodingConfig;

public record DeleteCodingConfigRequest(
    long Id
     ) : IHttpRequest;