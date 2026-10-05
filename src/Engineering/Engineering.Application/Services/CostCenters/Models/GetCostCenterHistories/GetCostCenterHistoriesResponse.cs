namespace Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;

public record GetCostCenterHistoriesResponse(
    List<GetCostCenterHistoriesModel> Data,
    int RowCount
    );
public record GetCostCenterHistoriesModel
{
    public long Id { get; set; }
    public long CostCenterTypeId { get; set; }
    public string CostCenterTypeTitle { get; set; }
    public string CostCenterCode { get; set; }
    public string CostCenterName { get; set; }
    public int? NoOperationDays { get; set; }
    public string Address { get; set; }
    public string? PostalCode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? Description { get; set; }
    public bool? WeatherState { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public Guid PreferentialReferenceCode { get; set; }
}
