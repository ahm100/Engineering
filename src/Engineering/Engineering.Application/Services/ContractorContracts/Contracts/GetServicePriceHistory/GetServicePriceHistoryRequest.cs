namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;

public record GetServicePriceHistoryRequest(
    long ServiceInfoId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
