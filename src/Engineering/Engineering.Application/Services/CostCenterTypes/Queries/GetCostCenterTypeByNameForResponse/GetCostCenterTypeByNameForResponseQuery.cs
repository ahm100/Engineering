using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByNameForResponse;

public record GetCostCenterTypeByNameForResponseQuery(
        string CostCenterTypeName) : IQuery<GetCostCenterTypeByNameResponse?>;