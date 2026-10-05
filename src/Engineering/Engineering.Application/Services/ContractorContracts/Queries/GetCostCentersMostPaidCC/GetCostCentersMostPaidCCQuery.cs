using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostPaidCC;

public record GetCostCentersMostPaidCCQuery(
    long CompanyId
    ) : IQuery<GetCostCentersMostPaidCCResponse?>;
