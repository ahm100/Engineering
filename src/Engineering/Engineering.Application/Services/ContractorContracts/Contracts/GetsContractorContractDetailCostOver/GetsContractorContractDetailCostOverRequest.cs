namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailCostOver;

public record GetsContractorContractDetailCostOverRequest(
    long ContractorContractHedearId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
