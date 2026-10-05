using Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;

namespace Engineering.Application.Services.CostCenters.Queries.GetsActiveMainWarehouseCostCenter;

public record GetsActiveMainWarehouseCostCenterQuery(
    string? FilterData,
    List<long>? WarehouseIds,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsActiveMainWarehouseCostCenterModel>>>;