

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractorPriceHistory;

public record GetContractorPriceHistoryRequest(
    long Id,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
