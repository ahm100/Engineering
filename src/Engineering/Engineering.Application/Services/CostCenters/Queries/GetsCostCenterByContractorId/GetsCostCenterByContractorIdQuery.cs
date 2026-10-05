
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsCostCenterByContractorId;

public record GetsCostCenterByContractorIdQuery(
    string? FilterData,
    long ContractorId,
    long? companyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<CostCenter>>>;
