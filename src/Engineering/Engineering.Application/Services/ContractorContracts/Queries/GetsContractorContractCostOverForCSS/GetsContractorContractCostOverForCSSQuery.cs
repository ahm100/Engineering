using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractCostOverForCSS;

public record GetsContractorContractCostOverForCSSQuery(
    long ProjectId,
    long ContractorId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractDetailCostOver>>>;
