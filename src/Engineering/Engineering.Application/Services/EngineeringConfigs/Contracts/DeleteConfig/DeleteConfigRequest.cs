namespace Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteConfig;

public record DeleteConfigRequest(
    long Id
     ) : IHttpRequest;