using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailPrice;

public record GetsContractorContractDetailPriceQuery(
    long ContractorContractHedearId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsContractorContractDetailPriceModel>>>;
