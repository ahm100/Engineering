namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;

public record GetsCostCenterByWarehouseRequest(
    long WarehouseId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
