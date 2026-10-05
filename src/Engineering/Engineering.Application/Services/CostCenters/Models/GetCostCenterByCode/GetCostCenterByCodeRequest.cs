namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByCode;

public record GetCostCenterByCodeRequest(
    string CostCenterCode
     ) : IHttpRequest;
