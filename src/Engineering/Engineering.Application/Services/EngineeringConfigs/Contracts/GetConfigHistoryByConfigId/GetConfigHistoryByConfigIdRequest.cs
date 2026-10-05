namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;

public record GetConfigHistoryByConfigIdRequest(
    long ConfigId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;