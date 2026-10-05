using CostCenterType = Engineering.Domain.Entities.CostCenters.CostCenterType;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeById;

public record GetCostCenterTypeByIdQuery(
    long Id)
    : IQuery<CostCenterType>;