namespace Engineering.Application.Services.CostOvers.Models.GetsActiveCostOvers;

public record GetsActiveCostOversModel
{
    public long Id { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
}