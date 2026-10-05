using Engineering.Application.Services.CostCenterWarehouses.Models.Models;

namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;

public record GetsCostCenterWarehouseByCostCenterIdsResponse(
    List<GetsCostCenterWarehouseByCostCenterIdsModel> Data
    );

public record GetsCostCenterWarehouseByCostCenterIdsModel(
    long CostCenterId,
    string CostCenterCode,
    string CostCenterName,
    List<CostCenterWarehouseModel> Data
    );
