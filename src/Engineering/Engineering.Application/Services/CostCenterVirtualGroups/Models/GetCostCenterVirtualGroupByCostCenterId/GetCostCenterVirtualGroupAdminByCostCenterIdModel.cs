namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;

public record GetCostCenterVirtualGroupAdminByCostCenterIdModel
{
    public long CostCenterVirtualGroupAdminId { get; set; }
    public long ThirdPartyId { get; set; }
    public string? ThirdPartyName { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
}