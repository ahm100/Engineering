
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostRecentCC;

public record GetCostCentersMostRecentQuery(
    long CompanyId
    ) : IQuery<GetCostCentersMostRecentCCResponse?>;
