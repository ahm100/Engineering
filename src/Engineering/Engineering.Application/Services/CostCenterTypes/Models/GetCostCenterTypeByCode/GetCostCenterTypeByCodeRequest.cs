namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByCode;

public record GetCostCenterTypeByCodeRequest(
    string CostCenterTypeCode)
    : IHttpRequest;