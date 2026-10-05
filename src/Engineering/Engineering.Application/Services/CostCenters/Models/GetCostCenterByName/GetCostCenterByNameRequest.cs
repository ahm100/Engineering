namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterByName;

public record GetCostCenterByNameRequest(
    string CostCenterName
     ) : IHttpRequest;
