using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailCostOver;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailCostOver;

public record GetsContractorContractDetailCostOverQuery(
    long ContractorContractHedearId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsContractorContractDetailCostOverModel>>>;
