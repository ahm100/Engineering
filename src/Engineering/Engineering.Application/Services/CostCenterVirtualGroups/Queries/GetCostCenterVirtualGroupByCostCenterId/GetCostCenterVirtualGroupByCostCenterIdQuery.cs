using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterVirtualGroups.Queries.CostCenterVirtualGroupById;

public record GetCostCenterVirtualGroupByCostCenterIdQuery(long CostCenterId,
                                                           int PageIndex,
                                                           int PageSize) : IQuery<DataResult<List<CostCenterVirtualGroup>>>;

