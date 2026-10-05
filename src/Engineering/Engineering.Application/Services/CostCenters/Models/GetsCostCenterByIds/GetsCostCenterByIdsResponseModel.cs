
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;

public record GetsCostCenterByIdsResponseModel
{
    public long Id { get; set; }
    public string CostCenterCode { get; set; } = string.Empty;
    public string CostCenterName { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public bool? IsDefault { get; set; }
    public Guid PreferentialReferenceCode { get; set; }
}