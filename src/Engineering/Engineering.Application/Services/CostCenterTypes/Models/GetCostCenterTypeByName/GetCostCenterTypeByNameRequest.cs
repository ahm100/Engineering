namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeByName;

public record GetCostCenterTypeByNameRequest(
    string CostCenterTypeName)
    : IHttpRequest;