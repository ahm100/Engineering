using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetContractorCostCenters;

public record GetContractorCostCentersQuery(
    long ContractorId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;