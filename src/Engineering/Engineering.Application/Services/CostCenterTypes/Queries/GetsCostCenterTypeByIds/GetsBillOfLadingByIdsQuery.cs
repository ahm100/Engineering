using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterTypeByIds;

public record GetsCostCenterTypeByIdsQuery(
    List<long> Items)
    : IQuery<List<CostCenterType>>;