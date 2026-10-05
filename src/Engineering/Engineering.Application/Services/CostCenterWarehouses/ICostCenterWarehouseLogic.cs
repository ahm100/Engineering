using Engineering.Application.Services.CostCenterWarehouses.Models.CostCenterWarehouseGroupDelete;
using Engineering.Application.Services.CostCenterWarehouses.Models.Create;
using Engineering.Application.Services.CostCenterWarehouses.Models.Creates;
using Engineering.Application.Services.CostCenterWarehouses.Models.Delete;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetById;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetDefaultCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;
using Engineering.Application.Services.CostCenterWarehouses.Models.Update;

namespace Engineering.Application.Services.CostCenterWarehouses;

public interface ICostCenterWarehouseLogic
{
    Task<Result<CreateCostCenterWarehouseResponse?>> CreateCostCenterWarehouse(
        CreateCostCenterWarehouseRequest request, CT ct);

    Task<Result<CreatesCostCenterWarehouseResponse?>> CreateCostCenterWarehouses(
        CreatesCostCenterWarehouseRequest request, CT ct);

    Task<Result<UpdateCostCenterWarehouseResponse?>> UpdateCostCenterWarehouse(
        UpdateCostCenterWarehouseRequest request, CT ct);

    Task<Result<DeleteCostCenterWarehouseResponse?>> DeleteCostCenterWarehouse(
        DeleteCostCenterWarehouseRequest request, CT ct);

    Task<Result<CostCenterWarehouseGroupDeleteResponse?>> CostCenterWarehouseGroupDelete(
        CostCenterWarehouseGroupDeleteRequest request, CT ct);

    Task<Result<GetCostCenterWarehouseByIdResponse?>> GetCostCenterWarehouseById(
        GetCostCenterWarehouseByIdRequest request, CT ct);

    Task<Result<GetsCostCenterWarehouseByCostCenterIdsResponse?>> GetsCostCenterWarehouseByCostCenterIds(
        GetsCostCenterWarehouseByCostCenterIdsRequest request, CT ct);

    Task<Result<GetsCostCenterWarehouseAssetsResponse?>> GetsCostCenterWarehouseAssets(
        GetsCostCenterWarehouseAssetsRequest request, CT ct);

    Task<Result<GetsCostCenterWarehouseByCostCenterIdResponse?>> GetsCostCenterWarehouseByCostCenterId(
        GetsCostCenterWarehouseByCostCenterIdRequest request, CT ct);

    Task<Result<GetsCostCenterWarehouseInventoryResponse?>> GetsCostCenterWarehouseInventory(
        GetsCostCenterWarehouseInventoryRequest request, CT ct);

    Task<Result<GetDefaultCostCenterWarehouseByCostCenterIdResponse?>> GetDefaultCostCenterWarehouseByCostCenterId(
        GetDefaultCostCenterWarehouseByCostCenterIdRequest request, CT ct);

}