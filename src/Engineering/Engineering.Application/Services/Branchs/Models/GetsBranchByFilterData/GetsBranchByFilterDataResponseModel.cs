using Engineering.Application.Services.Branchs.Models.BranchModels;

namespace Engineering.Application.Services.Branchs.Models.GetsBranchByFilterData;

public record GetsBranchByFilterDataResponseModel
{
    public long Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public List<SeasonModel>? Seasons { get; set; }
}