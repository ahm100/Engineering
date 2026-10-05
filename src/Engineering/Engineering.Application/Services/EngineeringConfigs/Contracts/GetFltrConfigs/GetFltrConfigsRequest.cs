namespace Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;

public record GetFltrConfigsRequest(
    bool? SendTelegramMessage,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
