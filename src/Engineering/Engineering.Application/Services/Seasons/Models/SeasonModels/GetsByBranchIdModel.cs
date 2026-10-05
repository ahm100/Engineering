namespace Engineering.Application.Services.Seasons.Models.SeasonModels;

public record GetsByBranchIdModel
{
    public long Id { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public string SeasonCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
