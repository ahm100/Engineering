namespace Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;

public record GetsActiveMainWarehouseCostCenterRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
