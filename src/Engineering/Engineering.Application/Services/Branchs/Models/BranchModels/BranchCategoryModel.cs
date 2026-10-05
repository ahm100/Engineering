namespace Engineering.Application.Services.Branchs.Models.BranchModels;

public record BranchCategoryModel
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}