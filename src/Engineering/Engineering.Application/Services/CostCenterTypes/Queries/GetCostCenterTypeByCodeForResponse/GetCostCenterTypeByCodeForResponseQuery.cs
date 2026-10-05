using Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;

namespace Engineering.Application.Services.CostCenterTypes.Queries.GetCostCenterTypeByCodeForResponse;

public record GetCostCenterTypeByCodeForResponseQuery(
    string CostCenterTypeCode) : IQuery<GetCostCenterTypeByCodeResponse?>;