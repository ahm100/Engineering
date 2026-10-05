namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetById;

public record GetCostCenterWarehouseByIdRequest(
    long CostCenterWarehouseId
     ) : IHttpRequest;
