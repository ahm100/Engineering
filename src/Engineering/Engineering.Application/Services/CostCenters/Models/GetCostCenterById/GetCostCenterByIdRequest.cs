namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterById;

public record GetCostCenterByIdRequest(
    long Id
     ) : IHttpRequest;
