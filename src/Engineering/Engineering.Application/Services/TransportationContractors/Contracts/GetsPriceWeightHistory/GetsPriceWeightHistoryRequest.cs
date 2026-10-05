namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;

public record GetsPriceWeightHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;