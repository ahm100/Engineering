namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigById;

public record GetConfigByIdRequest(
    long Id
    ) : IHttpRequest;