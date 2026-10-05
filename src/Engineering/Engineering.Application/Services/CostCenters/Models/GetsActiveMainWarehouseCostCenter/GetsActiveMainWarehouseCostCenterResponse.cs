
namespace Engineering.Application.Services.CostCenters.Models.GetsActiveMainWarehouseCostCenter;

public record GetsActiveMainWarehouseCostCenterResponse(
    List<GetsActiveMainWarehouseCostCenterModel> Data,
    int RowCount);

public record GetsActiveMainWarehouseCostCenterModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public List<long>? Ids { get; set; }
}


