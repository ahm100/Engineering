using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseForSupplyManagement;

public record GetsCostCenterWarehouseForSupplyManagementQuery(
    ) : IQuery<DataResult<List<CostCenterWarehouse>>>;