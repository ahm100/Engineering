using Engineering.Application.Services.Branchs.Models.BranchModels;

namespace Engineering.Application.Services.Branchs.Models.GetBranchByName;

public record GetBranchByNameResponse
{
    public long Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string AlternativeId { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public string CategoryCode { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public BranchCategoryModel? Category { get; set; }
}