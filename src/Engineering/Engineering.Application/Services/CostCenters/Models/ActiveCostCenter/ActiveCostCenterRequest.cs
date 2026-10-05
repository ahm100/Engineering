namespace Engineering.Application.Services.CostCenters.Models.ActiveCostCenter;

public record ActiveCostCenterRequest(
    long Id
     ) : IHttpRequest;
