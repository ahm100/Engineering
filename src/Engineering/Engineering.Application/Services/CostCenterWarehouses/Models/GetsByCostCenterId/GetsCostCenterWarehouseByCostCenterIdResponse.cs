using Engineering.Application.Services.CostCenterWarehouses.Models.Models;

namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsByCostCenterId;

public record GetsCostCenterWarehouseByCostCenterIdResponse(
    List<CostCenterWarehouseModel> Data,
    int RowCount);
