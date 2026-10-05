namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;

public record GetFltrCodingConfigsRequest(
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
