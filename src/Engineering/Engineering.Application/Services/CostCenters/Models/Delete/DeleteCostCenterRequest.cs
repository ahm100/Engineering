namespace Engineering.Application.Services.CostCenters.Models.Delete;

public record DeleteCostCenterRequest(
    long Id
     ) : IHttpRequest;
