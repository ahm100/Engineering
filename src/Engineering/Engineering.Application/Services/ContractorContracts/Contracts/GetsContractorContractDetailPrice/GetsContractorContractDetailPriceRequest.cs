namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;

public record GetsContractorContractDetailPriceRequest(
    long ContractorContractHedearId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
