namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;

public record GetCostCenterVirtualGroupByCostCenterIdModel
{
    public long CostCenterVirtualGroupId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string Identifier { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool SendToday { get; set; }
    public TimeSpan? TodayTime { get; set; }
    public bool SendYesterday { get; set; }
    public TimeSpan? YesterdayTime { get; set; }
    public List<GetCostCenterVirtualGroupAdminByCostCenterIdModel> Admins { get; set; } = new();
}
