namespace Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

public record GetsSeasonByBranchIdsModel
{
    public long Id { get; set; }
    public long BranchId { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public string SeasonCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}