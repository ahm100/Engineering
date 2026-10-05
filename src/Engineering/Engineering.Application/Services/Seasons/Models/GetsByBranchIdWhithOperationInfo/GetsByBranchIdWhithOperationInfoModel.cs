namespace Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;

public record GetsByBranchIdWhithOperationInfoModel
{
    public long Id { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public string SeasonCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}