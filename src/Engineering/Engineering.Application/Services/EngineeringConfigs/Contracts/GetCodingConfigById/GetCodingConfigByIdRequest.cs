namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;

public record GetCodingConfigByIdRequest(
    long Id
    ) : IHttpRequest;