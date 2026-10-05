namespace Engineering.Application.Services.CostCenters.Models.InactiveCostCenter;

public record InactiveCostCenterRequest(
    long Id
     ) : IHttpRequest;
