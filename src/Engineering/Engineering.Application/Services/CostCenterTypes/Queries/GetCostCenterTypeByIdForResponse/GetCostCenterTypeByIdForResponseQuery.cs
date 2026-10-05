using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByIdForResponse;

public record GetCostCenterTypeByIdForResponseQuery(
    long Id) : IQuery<GetCostCenterTypeByIdResponse?>;