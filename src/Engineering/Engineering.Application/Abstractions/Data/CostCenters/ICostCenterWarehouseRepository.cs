using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Abstractions.Data.CostCenters;

public interface ICostCenterWarehouseRepository : IBaseRepository<CostCenterWarehouse>
{
    Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseByCostCenter(
        long costCenterId, int pageIndex, int pageSize, CT ct);

    Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseByCostCenterIds(
        List<long> costCenterIds, int pageIndex, int pageSize, CT ct);

    Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseInventory(
        long costCenterId, int pageIndex, int pageSize, CT ct);

    Task<CostCenterWarehouse?> FindCostCenterWarehouse(
        long id, CT ct);

    Task<CostCenterWarehouse?> FindForUpdate(
        long id, CT ct);

    Task<(List<CostCenterWarehouse> Data, int RowCount)> GetsCostCenterWarehouseForSupplyManagement(
        CT ct);

    Task<List<CostCenterWarehouse>> GetByWarehouseIds(
        List<long> warehouseIds, CT ct);

    Task<CostCenterWarehouse?> GetDefaultByCostCenterId(
        long costCenterId, CT ct);

    Task<List<CostCenterWarehouse>> GetDefaultByCostCenterIds(
        List<long> costCenterIds, CT ct);

    Task<List<CostCenterWarehouse>?> FindWarehouseByCostCenter(
        long id, CT ct);

    Task<List<long>?> GetWarehouseIds(
        long costCenterId, CT ct);
}

