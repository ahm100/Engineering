
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByWarehouse;

public record GetsCostCenterByWarehouseResponse(
    List<GetsCostCenterByWarehouseModel> Data,
    int RowCount);

public record GetsCostCenterByWarehouseModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
    public List<GetsCostCenterByWarehouseModelProject>? Projects { get; set; }
}

public record GetsCostCenterByWarehouseModelProject
{
    public long Id { get; set; }
    public string ProjectCode { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public Guid? PreferentialReferenceCode { get; set; }
}
