
namespace Engineering.Application.Services.CostCenters.Models.CostCenterModels;

public record GetCostCentersModel
{
    public long Id { get; set; }
    public string CostCenterTypeTitle { get; set; } = string.Empty;
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public string? CostCenterEnName { get; set; } = string.Empty;
    public long? CostCenterWarehouseId { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; } = string.Empty;
    public string? WarehouseCode { get; set; } = string.Empty;
    public string? WarehouseManagement { get; set; } = string.Empty;
    public int? NoOperationDays { get; set; }
    public long CityId { get; set; }
    public string? CityName { get; set; } = string.Empty;
    public string? CityCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public bool? WeatherState { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public DateTime Created { get; set; }
};