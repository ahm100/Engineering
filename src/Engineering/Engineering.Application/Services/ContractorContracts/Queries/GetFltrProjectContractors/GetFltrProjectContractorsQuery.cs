using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFltrProjectContractors;

public record GetFltrProjectContractorsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize,
    long CompanyId
    ) : IQuery<GetFltrProjectContractorsResponse?>;
