
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelExporter;

public record GetsCostCenterExcelExporterModel
{
    public long Id { get; set; }
    public string CostCenterTypeTitle { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; } = string.Empty;
    public string? WarehouseManagement { get; set; } = string.Empty;
    public int? NoOperationDays { get; set; }
    public long CityId { get; set; }
    public string? CityName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool WeatherState { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
