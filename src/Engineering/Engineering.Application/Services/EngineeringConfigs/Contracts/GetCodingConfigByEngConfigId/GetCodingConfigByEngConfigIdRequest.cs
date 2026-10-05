namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;

public record GetCodingConfigByEngConfigIdRequest(
    long ConfigId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;