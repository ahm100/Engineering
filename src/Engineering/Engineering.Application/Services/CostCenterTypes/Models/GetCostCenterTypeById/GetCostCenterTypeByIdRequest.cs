namespace Engineering.Application.Services.CostCenterTypes.Models.GetCostCenterTypeById;

public record GetCostCenterTypeByIdRequest(
    long Id)
    : IHttpRequest;